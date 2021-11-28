using System;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Synnotech.DatabaseAbstractions;

namespace WpfDataGrid;

public sealed class InMemoryContactsSessionFactory : ISessionFactory<IGetContactsSession>
{
    public InMemoryContactsSessionFactory(InMemoryGetContactsSession session) => Session = session;

    private InMemoryGetContactsSession Session { get; }

    public ValueTask<IGetContactsSession> OpenSessionAsync(CancellationToken cancellationToken = default) => new (Session);

    public static InMemoryContactsSessionFactory CreateDefault(Random random)
    {
        var idCounter = 1;
        var contacts = new Faker<Contact>().StrictMode(true)
                                           .RuleFor(c => c.Id, () => idCounter++)
                                           .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                                           .RuleFor(c => c.LastName, f => f.Name.LastName())
                                           .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
                                           .RuleFor(c => c.DateOfBirth, f => f.Date.BetweenDateOnly(new DateOnly(1970, 1, 1), new DateOnly(2000, 12, 31)))
                                           .GenerateBetween(1000, 1000);
        return new (new (contacts, random));
    }
}