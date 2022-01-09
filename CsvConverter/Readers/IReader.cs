using CsvConverter.Converter;

namespace CsvConverter.Readers;

public interface IReader
{
    string ReaderType { get; }
    
    IEnumerable<IDataNode> Read(string input);
}