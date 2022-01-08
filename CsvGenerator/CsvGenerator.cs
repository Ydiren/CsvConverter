using System.Text.Json;
using Bogus;
using CsvGenerator.Models;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace CsvGenerator;

public class CsvGenerator
{
    private const string DefaultOutputFilename = "test.csv";
    
    private readonly IRowGenerator _rowGenerator;
    private readonly ILogger<CsvGenerator> _logger;

    public CsvGenerator(IRowGenerator rowGenerator,
        ILogger<CsvGenerator> logger)
    {
        _rowGenerator = rowGenerator;
        _logger = logger;
    }

    [Argument(0)]
    private int? RowsToGenerate { get; set; }
    
    [Argument(1)]
    private string? OutputFile { get; set; }

    public async Task OnExecuteAsync()
    {
        await GenerateAsync();
    }

    private async Task GenerateAsync()
    {
        if (string.IsNullOrWhiteSpace(OutputFile))
        {
            _logger.LogInformation($"{nameof(OutputFile)} is empty. Writing to '{DefaultOutputFilename}' by default.");
            OutputFile = DefaultOutputFilename;
        }

        var numberOfRows = RowsToGenerate ?? 10;

        var customers = _rowGenerator.GenerateCustomers(numberOfRows);

        await JsonSerializer.SerializeAsync(Console.OpenStandardOutput(), customers, new JsonSerializerOptions { WriteIndented = true });
    }
}