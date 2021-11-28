using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Synnotech.DatabaseAbstractions;

namespace WpfDataGrid;

public interface IGetContactsSession : IAsyncReadOnlySession
{
    Task<List<Contact>> GetContactsAsync(int skip,
                                         int take,
                                         string? searchTerm,
                                         SortInfo sortInfo,
                                         CancellationToken cancellationToken = default);
}