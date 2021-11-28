using System;
using System.ComponentModel;
using System.Windows.Controls;
using Light.GuardClauses.FrameworkExtensions;

namespace WpfDataGrid;

public static class DataGridExtensions
{
    public static void UpdateDataGridColumnsWithSortInfo(this DataGrid dataGrid, SortInfo sortInfo)
    {
        for (var i = 0; i < dataGrid.Columns.Count; i++)
        {
            var column = dataGrid.Columns[i];
            column.SortDirection = sortInfo.FieldName.Equals(column.Header) ? sortInfo.GetListSortDirection() : null;
        }
    }

    public static ListSortDirection GetListSortDirection(this SortInfo sortInfo) =>
        sortInfo.IsAscending ? ListSortDirection.Ascending : ListSortDirection.Descending;

    public static ListSortDirection GetNextSortDirection(this DataGridColumn dataGridColumn, string currentSortField) =>
        dataGridColumn.SortDirection switch
        {
            ListSortDirection.Ascending => ListSortDirection.Descending,
            ListSortDirection.Descending => ListSortDirection.Ascending,
            null => currentSortField.Equals(dataGridColumn.Header) ? ListSortDirection.Descending : ListSortDirection.Ascending,
            _ => throw new ArgumentOutOfRangeException(nameof(dataGridColumn), $"Unknown value {dataGridColumn.SortDirection.ToStringOrNull()} for ListSortDirection")
        };

    public static bool ConvertToIsAscendingSort(this ListSortDirection sortDirection) =>
        sortDirection == ListSortDirection.Ascending;
}