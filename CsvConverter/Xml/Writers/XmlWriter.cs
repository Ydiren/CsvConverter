using System.IO.Abstractions;
using System.Security;
using Common.Models;
using CsvConverter.Interfaces;
using CsvConverter.Xml.Services;
using Microsoft.Extensions.Logging;

namespace CsvConverter.Xml.Writers;

public class XmlWriter : IWriter
{
    private readonly IFileSystem _fileSystem;
    private readonly ILogger<XmlWriter> _logger;
    private readonly IXmlSerializationService _xmlSerializationService;
    public string Type => "xml";

    public XmlWriter(IFileSystem fileSystem,
                     ILogger<XmlWriter> logger, 
                     IXmlSerializationService xmlSerializationService)
    {
        _fileSystem = fileSystem;
        _logger = logger;
        _xmlSerializationService = xmlSerializationService;
    }

    public async Task WriteAsync(string outputFilename, IEnumerable<PersonDetail> peopleDetails)
    {
        var filename = new FileName(outputFilename);

        try
        {
            await using var fileStream = _fileSystem.FileStream.Create(filename.FullPath, FileMode.Create);
            var personDetails = peopleDetails.ToList();
            _xmlSerializationService.Serialize(fileStream, personDetails);

            _logger.LogInformation("Successfully written {RecordCount} records to {Filename}", 
                                   personDetails.Count,
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