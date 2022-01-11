using System.IO.Abstractions;
using System.Text.Json;
using Common.Models;
using CsvConverter.Interfaces;

namespace CsvConverter.Json.Writers;

public class JsonWriter : IWriter
{
    private readonly IFileSystem _fileSystem;

    public JsonWriter(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public string Type => "json";

    public async Task WriteAsync(string outputFilename, IEnumerable<PersonDetail> peopleDetails)
    {
        var filename = new FileName(outputFilename);

        await using var fileStream = _fileSystem.FileStream.Create(filename.FullPath,
                                                                   FileMode.Create);
        await JsonSerializer.SerializeAsync(fileStream,
                                            peopleDetails,
                                            new JsonSerializerOptions
                                            {
                                                WriteIndented = true
                                            });
    }
}