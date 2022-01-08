using CsvConverter.Readers;

namespace CsvConverter;

public interface IReaderFactory
{
    IReader Create(string input, string type);
}

public class ReaderFactory : IReaderFactory
{
    public IReader Create(string input, string type)
    {
        return null;
    }
}