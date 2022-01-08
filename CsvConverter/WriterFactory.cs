namespace CsvConverter;

public interface IWriterFactory
{
    IWriter Create(string output, string type);
}

public class WriterFactory : IWriterFactory
{
    public IWriter Create(string output, string type)
    {
        return null;
    }
}