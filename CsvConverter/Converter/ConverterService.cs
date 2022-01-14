using CsvConverter.Repositories;
using Microsoft.Extensions.Logging;

namespace CsvConverter.Converter;

internal interface IConverterService
{
    Task ConvertAsync(ConverterParameters parameters);
}

internal class ConverterService : IConverterService
{
    private readonly ILogger<ConverterService> _logger;
    private readonly IReaderRepository _readerRepository;
    private readonly IWriterRepository _writerRepository;

    public ConverterService(ILogger<ConverterService> logger, IReaderRepository readerRepository,
                            IWriterRepository writerRepository)
    {
        _logger = logger;
        _readerRepository = readerRepository;
        _writerRepository = writerRepository;
    }

    public async Task ConvertAsync(ConverterParameters parameters)
    {
        _logger.LogInformation("{ClassName}.{MethodName} Called with parameters: {parameters}",
                               nameof(ConverterService),
                               nameof(ConvertAsync),
                               parameters);

        // Retrieve reader and writer from repository class
        var reader = _readerRepository.Get(parameters.InputType);
        var writer = _writerRepository.Get(parameters.OutputType);

        var peopleDetails = await reader.ReadAsync(parameters.Input);
        await writer.WriteAsync(parameters.Output,
                                peopleDetails);
    }
}