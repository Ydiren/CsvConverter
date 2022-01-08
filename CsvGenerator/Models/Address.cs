namespace CsvGenerator.Models;

public record Address
{
    public string Line1 { get; init; } = string.Empty;
    public string Line2 { get; init; } = string.Empty;
    public string Line3 { get; init; } = string.Empty;
}