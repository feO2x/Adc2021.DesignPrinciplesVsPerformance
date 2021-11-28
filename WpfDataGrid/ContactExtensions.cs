using System;
using System.Collections.Generic;
using Light.GuardClauses.FrameworkExtensions;

namespace WpfDataGrid;

public static class ContactExtensions
{
    public static IEnumerable<Contact> OrderBy(this IEnumerable<Contact> contacts,
                                               SortInfo sortInfo)
    {
        var isAscending = sortInfo.IsAscending;
        return sortInfo.FieldName switch
        {
            nameof(Contact.Id) => contacts.OrderBy(c => c.Id, isAscending),
            nameof(Contact.FirstName) => contacts.OrderBy(c => c.FirstName, isAscending),
            nameof(Contact.LastName) => contacts.OrderBy(c => c.LastName, isAscending),
            nameof(Contact.DateOfBirth) => contacts.OrderBy(c => c.DateOfBirth, isAscending),
            nameof(Contact.Email) => contacts.OrderBy(c => c.Email, isAscending),
            _ => throw new ArgumentException($"The field {sortInfo.FieldName.ToStringOrNull()} is unknown", nameof(sortInfo))
        };
    }
}