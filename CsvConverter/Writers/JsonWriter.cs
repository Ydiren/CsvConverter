using CsvConverter.Converter;

namespace CsvConverter.Writers;

public class JsonWriter : IWriter
{
    public string Type { get; }
    public void Write(IEnumerable<IDataNode> data)
    {
        throw new NotImplementedException();
    }
}