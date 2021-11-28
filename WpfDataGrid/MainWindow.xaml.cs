using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Light.GuardClauses;

namespace WpfDataGrid;

public sealed partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    public MainWindow(MainWindowViewModel viewModel) : this() => DataContext = viewModel;

    private MainWindowViewModel ViewModel =>
        DataContext as MainWindowViewModel ??
        throw new InvalidOperationException("The view model must be set as the DataContext before accessing it");

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is not MainWindowViewModel viewModel)
            return;

        DataGrid.UpdateDataGridColumnsWithSortInfo(viewModel.SortInfo);
        viewModel.PropertyChanged += OnViewModelPropertyChanged;
        DataGrid.Sorting += OnSorting;
        DataGrid.AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(OnScrollChanged));
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainWindowViewModel.SortInfo))
            DataGrid.UpdateDataGridColumnsWithSortInfo(ViewModel.SortInfo);
    }

    private void OnSorting(object sender, DataGridSortingEventArgs e)
    {
        e.Handled = true;

        var viewModel = ViewModel;
        var sortField = e.Column.Header.MustBeOfType<string>();
        var isAscendingSort = e.Column
                               .GetNextSortDirection(viewModel.SortInfo.FieldName)
                               .ConvertToIsAscendingSort();
        viewModel.SortInfo = new (sortField, isAscendingSort);
        viewModel.GetContacts();
    }

    private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (e.VerticalChange == 0)
            return;

        if (Math.Abs(e.VerticalOffset + e.ViewportHeight - e.ExtentHeight) < 0.001)
            ViewModel.GetNextContacts();
    }
}