using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WpfDataGrid;

public sealed class GetContactsOperation
{
    public GetContactsOperation(MainWindowViewModel viewModel,
                                int skip,
                                int take)
    {
        ViewModel = viewModel;
        Skip = skip;
        Take = take;
    }

    private MainWindowViewModel ViewModel { get; }
    public int Skip { get; }
    public int Take { get; }
    private CancellationTokenSource? CancellationTokenSource { get; set; }

    public async Task GetContactsAsync()
    {
        if (CancellationTokenSource != null)
            throw new InvalidOperationException("GetContactsAsync must only be called once per operation");

        CancellationTokenSource = new CancellationTokenSource();
        IGetContactsSession? session = null;
        try
        {
            session = await ViewModel.SessionFactory.OpenSessionAsync(CancellationTokenSource.Token);
            var contacts = await session.GetContactsAsync(Skip,
                                                          Take,
                                                          ViewModel.SearchTerm,
                                                          ViewModel.SortInfo,
                                                          CancellationTokenSource.Token);
            if (Skip == 0)
                ViewModel.Contacts.Clear();

            foreach (var contact in contacts)
            {
                ViewModel.Contacts.Add(contact);
            }
        }
        catch (OperationCanceledException exception)
        {
            ViewModel.Logger.Information(exception, "The operation was cancelled");

        }
        catch (Exception exception)
        {
            ViewModel.Logger.Error(exception, "Could not load contacts");
        }
        finally
        {
            if (session != null)
                await session.DisposeAsync();
            CancellationTokenSource?.Dispose();
        }
    }

    public void Cancel()
    {
        var cancellationTokenSource = CancellationTokenSource;
        if (cancellationTokenSource is { IsCancellationRequested: false })
            cancellationTokenSource.Cancel();
    }
}