using CsvConverter.Interfaces;

namespace CsvConverter.Repositories;

public interface IReaderRepository : IRepository<IReader>
{
}

public class ReaderRepository : IReaderRepository
{
    private readonly Dictionary<string, IReader> _readers = new();

    public void Add(IReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);

        _readers.Add(reader.ReaderType,
                     reader);
    }

    public IReader Get(string type)
    {
        if (!_readers.TryGetValue(type,
                                  out var reader))
            throw new InvalidOperationException($"Unsupported reader type: '{type}");

        return reader;
    }
}