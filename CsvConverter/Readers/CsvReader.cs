using CsvConverter.Converter;

namespace CsvConverter.Readers;

public class CsvReader : IReader
{
    public string ReaderType => "csv";

    public IEnumerable<IDataNode> Read(string input)
    {
        throw new NotImplementedException();
    }
}