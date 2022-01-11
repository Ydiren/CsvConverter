using System.Globalization;
using Common.Models;
using CsvHelper;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace CsvGenerator;

public class CsvGenerator
{
    private const string DefaultOutputFilename = "test.csv";
    private readonly ILogger<CsvGenerator> _logger;

    private readonly IRowGenerator _rowGenerator;

    public CsvGenerator(IRowGenerator rowGenerator, ILogger<CsvGenerator> logger)
    {
        _rowGenerator = rowGenerator;
        _logger = logger;
        OutputFile = string.Empty;
    }

    [Argument(0)] private int? RowsToGenerate { get; set; }

    [Argument(1)] private string OutputFile { get; }

    public async Task OnExecuteAsync()
    {
        await GenerateAsync();
    }

    private async Task GenerateAsync()
    {
        var filename = CreateOutputFilename();
        var numberOfRows = RowsToGenerate ?? 10;
        var customers = _rowGenerator.GenerateCustomers(numberOfRows);

        try
        {
            await WriteCsvFile(filename,
                               customers);

            _logger.LogInformation("Successfully written {NumberOfRows} rows to '{Filename}'",
                                   numberOfRows,
                                   filename);
        }
        catch (Exception exception)
        {
            _logger.LogCritical(exception,
                                "Failed to create output file '{Filename}'",
                                filename);
        }
    }

    private static async Task WriteCsvFile(FileName filename, IEnumerable<CsvRow> customers)
    {
        await using var writer = new StreamWriter(filename.FullPath);
        await using var csvWriter = new CsvWriter(writer,
                                                  CultureInfo.InvariantCulture);
        await csvWriter.WriteRecordsAsync(customers);
    }

    private FileName CreateOutputFilename()
    {
        try
        {
            return new FileName(OutputFile);
        }
        catch (ArgumentNullException)
        {
            _logger.LogInformation("{OutputFile} is invalid. Writing to '{DefaultOutputFilename}' by default",
                                   OutputFile,
                                   DefaultOutputFilename);
            return new FileName(DefaultOutputFilename);
        }
    }
}