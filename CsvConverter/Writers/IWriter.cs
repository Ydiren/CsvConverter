using CsvConverter.Converter;

namespace CsvConverter.Writers;

public interface IWriter
{
    string Type { get; }
    void Write(IEnumerable<IDataNode> data);
}