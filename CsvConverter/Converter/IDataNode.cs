namespace CsvConverter.Converter;

public interface IDataNode
{
    string Name { get; }
    string Value { get; }
    ICollection<IDataNode> Children { get; }
}