using System.ComponentModel.DataAnnotations;
using CsvConverter.Converter;
using CsvConverter.Repositories.Initializers;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace CsvConverter;

internal class Bootstrapper 
{
    private readonly CommandLineApplication _application;
    private readonly IConverterService _converterService;
    private readonly IEnumerable<IRepositoryInitializer> _repositoryInitializers;
    private readonly ILogger<Bootstrapper> _logger;

    public Bootstrapper(CommandLineApplication application,
        IConverterService converterService,
        IEnumerable<IRepositoryInitializer> repositoryInitializers,
        ILogger<Bootstrapper> logger)
    {
        _application = application;
        _converterService = converterService;
        _repositoryInitializers = repositoryInitializers;
        _logger = logger;
    }

    [Argument(0)]
    [Required]
    public string Input { get; set; }
    
    [Option(CommandOptionType.SingleValue)]
    [Required]
    public string InputType { get; set; }
    
    [Argument(1)]
    [Required]
    public string Output { get; set; }
    
    [Option(CommandOptionType.SingleValue)]
    [Required]
    public string OutputType { get; set; }
    
    public async Task OnExecuteAsync()
    {
        foreach (var initializer in _repositoryInitializers)
        {
            initializer.InitializeAll();
        }

        try
        {
            var parameters = new ConverterParameters(
                Input,
                InputType,
                Output,
                OutputType);
            await _converterService.ConvertAsync(parameters);
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError(e, $"Problem encountered while converting data.");
            _application.ShowHelp();
        }
    }
}