using CsvConverter.Readers;

namespace CsvConverter.Repositories.Initializers;

public class ReaderRepositoryInitializer : IRepositoryInitializer
{
    private readonly IReaderRepository _readerRepository;
    private readonly IEnumerable<IReader> _readers;

    public ReaderRepositoryInitializer(IReaderRepository readerRepository,
        IEnumerable<IReader> readers)
    {
        _readerRepository = readerRepository;
        _readers = readers;
    }

    public void InitializeAll()
    {
        foreach (var reader in _readers)
        {
            _readerRepository.Add(reader);
        }
    }
}