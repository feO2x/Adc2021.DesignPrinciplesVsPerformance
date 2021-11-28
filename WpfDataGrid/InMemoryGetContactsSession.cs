using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Light.GuardClauses;

namespace WpfDataGrid;

public sealed class InMemoryGetContactsSession : IGetContactsSession
{
    public InMemoryGetContactsSession(List<Contact> contacts, Random random)
    {
        Contacts = contacts;
        Random = random;
    }

    private List<Contact> Contacts { get; }
    private Random Random { get; }

    public async Task<List<Contact>> GetContactsAsync(int skip,
                                                      int take,
                                                      string? searchTerm,
                                                      SortInfo sortInfo,
                                                      CancellationToken cancellationToken = default)
    {
        IEnumerable<Contact> contacts = Contacts;
        if (!searchTerm.IsNullOrWhiteSpace())
        {
            contacts = contacts.Where(c => c.LastName.StartsWith(searchTerm) ||
                                           c.FirstName.StartsWith(searchTerm));
        }

        var result = contacts.OrderBy(sortInfo)
                             .Page(skip, take)
                             .ToList();

        var waitInterval = Random.Next(500, 1200);
        await Task.Delay(waitInterval, cancellationToken).ConfigureAwait(false);
        return result;
    }

    public void Dispose() { }

    public ValueTask DisposeAsync() => default;
}