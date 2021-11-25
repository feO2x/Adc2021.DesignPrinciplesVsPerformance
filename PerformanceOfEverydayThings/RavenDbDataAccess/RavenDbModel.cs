using System;
using System.Collections.Generic;

namespace PerformanceOfEverydayThings.RavenDbDataAccess;

public class Employee
{
    public string Id { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public Address Address { get; set; } = default!;
    public DateTime HiredAt { get; set; }
    public DateTime Birthday { get; set; }
    public string HomePhone { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string ReportsTo { get; set; } = string.Empty;
    public List<string> Notes { get; set; } = default!;
    public List<string> Territories { get; set; } = default!;
}

public class Address
{
    public string Line1 { get; set; } = string.Empty;
    public string Line2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public Location Location { get; set; } = default!;
}

public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}