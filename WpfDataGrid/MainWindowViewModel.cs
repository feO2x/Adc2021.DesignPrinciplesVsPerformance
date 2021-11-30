using System;
using System.Collections.ObjectModel;
using System.Windows;
using Light.ViewModels;
using Serilog;
using Synnotech.DatabaseAbstractions;

namespace WpfDataGrid;

public sealed class MainWindowViewModel : BaseNotifyPropertyChanged
{
    public const int Take = 50;
    private string _searchTerm = string.Empty;
    private SortInfo _sortInfo = new (nameof(Contact.Id), true);
    private GetContactsOperation? _currentOperation;
    private Visibility _loadingIndicatorVisibility;

    public MainWindowViewModel(ISessionFactory<IGetContactsSession> sessionFactory,
                               ILogger logger)
    {
        SessionFactory = sessionFactory;
        Logger = logger;
    }

    public ISessionFactory<IGetContactsSession> SessionFactory { get; }
    public ILogger Logger { get; }

    private GetContactsOperation? CurrentOperation
    {
        get => _currentOperation;
        set
        {
            _currentOperation = value;
            LoadingIndicatorVisibility = _currentOperation != null ? Visibility.Visible : Visibility.Hidden;
        }
    }

    public ObservableCollection<Contact> Contacts { get; } = new ();
    
    public string SearchTerm
    {
        get => _searchTerm;
        set
        {
            if (SetIfDifferent(ref _searchTerm, value))
                GetContacts();
        }
    }

    public SortInfo SortInfo
    {
        get => _sortInfo;
        set => SetIfDifferent(ref _sortInfo, value);
    }

    public Visibility LoadingIndicatorVisibility
    {
        get => _loadingIndicatorVisibility;
        private set => SetIfDifferent(ref _loadingIndicatorVisibility, value);
    }

    public void GetContacts() =>
        GetContactsInternal(new GetContactsOperation(this, 0, Take));

    public void GetNextContacts()
    {
        if (Contacts == null)
            throw new InvalidOperationException("GetNextContacts can only be called when some contacts were loaded beforehand.");

        if (CurrentOperation is { Skip: > 0 })
            return;

        GetContactsInternal(new GetContactsOperation(this, Contacts.Count, Take));
    }

    private async void GetContactsInternal(GetContactsOperation newOperation)
    {
        CurrentOperation?.Cancel();
        CurrentOperation = newOperation;
        await CurrentOperation.GetContactsAsync();
        if (ReferenceEquals(CurrentOperation, newOperation))
            CurrentOperation = null;
    }
}