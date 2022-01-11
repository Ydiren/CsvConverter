using CsvConverter.Writers;

namespace CsvConverter.Repositories.Initializers;

public class WriterRepositoryInitializer : IRepositoryInitializer
{
    private readonly IWriterRepository _writerRepository;
    private readonly IEnumerable<IWriter> _writers;

    public WriterRepositoryInitializer(IWriterRepository writerRepository,
    IEnumerable<IWriter> writers)
    {
        _writerRepository = writerRepository;
        _writers = writers;
    }

    public void InitializeAll()
    {
        foreach (var writer in _writers)
        {
            _writerRepository.Add(writer);
        }
    }
}