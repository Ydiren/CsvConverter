namespace CsvConverter.Readers;

public interface IReader
{
    object Read(string input);
}