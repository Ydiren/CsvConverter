using Common.Models;
using CsvConverter.Csv.Factories;
using CsvConverter.Csv.Services;
using CsvConverter.Interfaces;
using Microsoft.Extensions.Logging;

namespace CsvConverter.Csv.Readers;

internal class CsvReader : IReader
{
    private readonly ICsvReaderService _csvReaderService;
    private readonly ILogger<CsvReader> _logger;
    private readonly IPersonDetailsFactory _personDetailsFactory;

    public CsvReader(ICsvReaderService csvReaderService, ILogger<CsvReader> logger,
                     IPersonDetailsFactory personDetailsFactory)
    {
        _csvReaderService = csvReaderService;
        _logger = logger;
        _personDetailsFactory = personDetailsFactory;
    }

    public string ReaderType => "csv";

    public async Task<IEnumerable<PersonDetail>> ReadAsync(string inputLocation)
    {
        var filename = new FileName(inputLocation);

        try
        {
            var csvRows = await _csvReaderService.ReadAsync<CsvRow>(filename);
            
            _logger.LogInformation("Read {RecordCount} records from {Filename}",
                                   csvRows.Count(),
                                   filename);

            var peopleDetails = csvRows.Select(row => _personDetailsFactory.Create(row))
                                       .ToList();

            return peopleDetails;
        }
        catch (Exception e) when (e is DirectoryNotFoundException or FileNotFoundException or IOException)
        {
            _logger.LogCritical(e, 
                                "Could not read file '{Filename}'",
                                filename.FullPath);
            return Enumerable.Empty<PersonDetail>();
        }
        catch (ArgumentException e)
        {
            _logger.LogCritical(e,
                                "Invalid argument passed");
            return Enumerable.Empty<PersonDetail>();
        }
    }
}