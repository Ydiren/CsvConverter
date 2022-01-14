using CsvConverter.Interfaces;

namespace CsvConverter.Repositories;

internal interface IReaderRepository : IRepository<IReader>
{
    IEnumerable<string> SupportedTypes { get; }
}

internal class ReaderRepository : IReaderRepository
{
    private readonly Dictionary<string, IReader> _readers = new();

    public IEnumerable<string> SupportedTypes => _readers.Keys.ToList();

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