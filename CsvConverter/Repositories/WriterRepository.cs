using CsvConverter.Interfaces;

namespace CsvConverter.Repositories;

public interface IWriterRepository : IRepository<IWriter>
{
    IEnumerable<string> SupportedTypes { get; }
}

public class WriterRepository : IWriterRepository
{
    private readonly Dictionary<string, IWriter> _writers = new();

    public IEnumerable<string> SupportedTypes => _writers.Keys.ToList();

    public void Add(IWriter writer)
    {
        ArgumentNullException.ThrowIfNull(writer);

        _writers.Add(writer.WriterType,
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