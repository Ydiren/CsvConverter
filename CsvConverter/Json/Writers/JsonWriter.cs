using System.IO.Abstractions;
using System.Security;
using Common.Models;
using CsvConverter.Interfaces;
using Microsoft.Extensions.Logging;

namespace CsvConverter.Json.Writers;

public class JsonWriter : IWriter
{
    private readonly IFileSystem _fileSystem;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly ILogger<JsonWriter> _logger;

    public JsonWriter(IFileSystem fileSystem,
                      IJsonSerializerService jsonSerializerService,
                      ILogger<JsonWriter> logger)
    {
        _fileSystem = fileSystem;
        _jsonSerializerService = jsonSerializerService;
        _logger = logger;
    }

    public string Type => "json";

    public async Task WriteAsync(string outputFilename, IEnumerable<PersonDetail> peopleDetails)
    {
        var filename = new FileName(outputFilename);

        try
        {
            await using var fileStream = _fileSystem.FileStream.Create(filename.FullPath,
                                                                       FileMode.Create);

            await _jsonSerializerService.Serialize(fileStream, peopleDetails);
            _logger.LogInformation("Written {RecordCount} records to {Filename}",
                                   peopleDetails.Count(),
                                   filename);
        }
        catch (Exception e) when (e is SecurityException or UnauthorizedAccessException)
        {
            _logger.LogCritical(e,
                                "You don't have access to the output location. '{Filename}'",
                                filename);
        }
    }
}