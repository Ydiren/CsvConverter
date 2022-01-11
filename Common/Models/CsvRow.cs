using CsvHelper.Configuration.Attributes;

namespace Common.Models;

public class CsvRow
{
    public CsvRow()
    {
        Name = string.Empty;
        AddressLine1 = string.Empty;
        AddressLine2 = string.Empty;
        AddressLine3 = string.Empty;
    }

    [Name("name")] public string Name { get; init; }

    [Name("address_line1")] public string AddressLine1 { get; init; }

    [Name("address_line2")] public string AddressLine2 { get; init; }

    [Name("address_line3")] public string AddressLine3 { get; init; }
}