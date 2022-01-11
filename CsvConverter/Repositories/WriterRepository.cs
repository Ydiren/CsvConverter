using CsvConverter.Interfaces;

namespace CsvConverter.Repositories;

public interface IWriterRepository : IRepository<IWriter>
{
}

public class WriterRepository : IWriterRepository
{
    private readonly Dictionary<string, IWriter> _writers = new();

    public void Add(IWriter writer)
    {
        ArgumentNullException.ThrowIfNull(writer);

        _writers.Add(writer.Type,
                     writer);
    }

    public IWriter Get(string type)
    {
        if (!_writers.TryGetValue(type,
                                  out var writer))
            throw new InvalidOperationException($"Unsupported writer type: '{type}'.");

        return writer;
    }
}