using Common.Models;

namespace CsvConverter.Readers;

public class CsvReader : IReader
{
    public CsvReader(CsvHelper.IReader CsvReader)
    {
    }

    public string ReaderType => "csv";

    public IEnumerable<PersonDetail> Read(string inputLocation)
    {
        if (string.IsNullOrWhiteSpace(inputLocation))
        {
            throw new ArgumentException("Input location must not be empty or only whitespace.", nameof(inputLocation));
        }

        return new List<PersonDetail>();
    }
}