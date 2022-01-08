namespace CsvGenerator.Models;

public record Customer
{
    public string Name { get; init; } = string.Empty;
    public Address Address { get; init; } = new ();
}