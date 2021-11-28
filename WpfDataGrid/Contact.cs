using System;

namespace WpfDataGrid;

public sealed class Contact
{
    public int Id { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public DateOnly DateOfBirth { get; init; }

    public string Email { get; init; } = string.Empty;
}