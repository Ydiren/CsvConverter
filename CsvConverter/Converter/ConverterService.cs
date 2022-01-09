using CsvConverter.Repositories;
using Microsoft.Extensions.Logging;

namespace CsvConverter.Converter;

public interface IConverterService
{
    Task ConvertAsync(ConverterParameters parameters);
}

public class ConverterService : IConverterService
{
    private readonly ILogger<ConverterService> _logger;
    private readonly IReaderRepository _readerRepository;
    private readonly IWriterRepository _writerRepository;

    public ConverterService(ILogger<ConverterService> logger,
        IReaderRepository readerRepository,
        IWriterRepository writerRepository)
    {
        _logger = logger;
        _readerRepository = readerRepository;
        _writerRepository = writerRepository;
    }

    public async Task ConvertAsync(ConverterParameters parameters)
    {
        _logger.LogInformation($"{nameof(Bootstrapper)}.{nameof(ConvertAsync)} Called with input: '{parameters.Input}', output: '{parameters.Output}', input type: '{parameters.InputType}', output type: '{parameters.OutputType}'.");

        // Retrieve reader and writer from repository class
        var reader = _readerRepository.Get(parameters.InputType);
        var writer = _writerRepository.Get(parameters.OutputType);
        
        var readData = reader.Read(parameters.Input);
        writer.Write(readData);
        
        await Task.CompletedTask;
    }
}