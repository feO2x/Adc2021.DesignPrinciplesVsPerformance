using System.Linq;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using PerformanceOfEverydayThings.EfCoreDataAccess;
using Raven.Client.Documents;
using Employee = PerformanceOfEverydayThings.RavenDbDataAccess.Employee;

namespace PerformanceOfEverydayThings;

[InvocationCount(1024, 16)]
public class InputOutputBenchmarks
{
    public DbContextOptions<DatabaseContext>? DbContextOptions;
    public IDocumentStore? RavenDbStore;

    [Benchmark(Baseline = true)]
    public Employee? LoadEmployeeFromRavenDb()
    {
        using var session = RavenDbStore!.OpenSession();
        return session.Load<Employee>("employees/9-A");
    }

    [Benchmark]
    public Person? LoadPersonFromMsSqlViaEfCore()
    {
        using var context = new DatabaseContext(DbContextOptions);
        return context.People
                      .Include(p => p.BusinessEntity.BusinessEntityAddresses)
                      .Include(p => p.PersonPhones)
                      .FirstOrDefault(p => p.BusinessEntityId == 1663);
    }

    [GlobalSetup(Target = nameof(LoadEmployeeFromRavenDb))]
    public void SetupRavenDbConnection()
    {
        RavenDbStore = new DocumentStore
            {
                Urls = new[] { "http://localhost:10001" },
                Database = "AdventureWorks"
            }
           .Initialize();
    }

    [GlobalSetup(Target = nameof(LoadPersonFromMsSqlViaEfCore))]
    public void SetupEntityFrameworkContext()
    {
        DbContextOptions =
            new DbContextOptionsBuilder<DatabaseContext>()
               .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=AdventureWorks2016;Integrated Security=True")
               .Options;
    }
}