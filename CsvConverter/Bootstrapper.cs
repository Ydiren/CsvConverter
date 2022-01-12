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
    private readonly ILogger<Bootstrapper> _logger;

    public Bootstrapper(CommandLineApplication application,
                        IConverterService converterService,
                        ICustomHelpTextGenerator helpTextGenerator,
                        IEnumerable<IRepositoryInitializer> repositoryInitializers,
                        ILogger<Bootstrapper> logger)
    {
        _application = application;
        _converterService = converterService;
        _logger = logger;

        foreach (var initializer in repositoryInitializers)
        {
            initializer.InitializeAll();
        }
        
        helpTextGenerator.AddToCommandLine(_application);
    }

    [Argument(0)] [Required] public string Input { get; set; } = string.Empty;

    [Option(CommandOptionType.SingleValue)]
    [Required]
    public string InputType { get; set; } = string.Empty;

    [Argument(1)] [Required] public string Output { get; set; } = string.Empty;

    [Option(CommandOptionType.SingleValue)]
    [Required]
    public string OutputType { get; set; } = string.Empty;

    public async Task OnExecuteAsync()
    {
        try
        {
            var parameters = new ConverterParameters(Input,
                                                     InputType,
                                                     Output,
                                                     OutputType);
            await _converterService.ConvertAsync(parameters);
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                             "Problem encountered while converting data");
            _application.ShowHelp();
        }
    }
}