using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfDataGrid;

public static class EnumerableExtensions
{
    public static IEnumerable<T> OrderBy<T, TKey>(this IEnumerable<T> source,
                                                  Func<T, TKey> selectKey,
                                                  bool isAscending) =>
        isAscending ? source.OrderBy(selectKey) : source.OrderByDescending(selectKey);

    public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int skip, int take) =>
        source.Skip(skip).Take(take);
}