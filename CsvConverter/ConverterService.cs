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
    
    [Argument(1)]
    [Required]
    public string Output { get; set; }
    
    [Option(CommandOptionType.SingleOrNoValue)]
    public string? Type { get; set; }

    public async Task OnExecuteAsync()
    {
        await ConvertAsync(Input, Output);
    }
    
    public async Task ConvertAsync(string input, string output)
    {
        _logger.LogInformation($"{nameof(ConverterService)}.{nameof(ConvertAsync)} Called with input: '{input}', output: '{output}', type: '{Type ?? "<null>"}'.");
        await Task.CompletedTask;
    }
}