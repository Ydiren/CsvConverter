using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace CsvConverter;

public class ConverterService 
{
    private readonly ILogger<ConverterService> _logger;

    public ConverterService(ILogger<ConverterService> logger)
    {
        _logger = logger;
    }

    [Argument(0)]
    [Required]
    public string Input { get; set; }
    
    [Option(CommandOptionType.SingleOrNoValue)]
    public string? InputType { get; set; }
    
    [Argument(1)]
    [Required]
    public string Output { get; set; }
    
    [Option(CommandOptionType.SingleOrNoValue)]
    public string? OutputType { get; set; }
    public async Task OnExecuteAsync()
    {
        await ConvertAsync(Input, Output);
    }
    
    public async Task ConvertAsync(string input, string output)
    {
        _logger.LogInformation($"{nameof(ConverterService)}.{nameof(ConvertAsync)} Called with input: '{input}', output: '{output}', type: '{InputType ?? "<null>"}'.");
        
        if (string.IsNullOrWhiteSpace(InputType))
        {
            _logger.LogInformation($"{nameof(InputType)} was not provided. Using file extensions to determine type.");
        }

        await Task.CompletedTask;
    }
}