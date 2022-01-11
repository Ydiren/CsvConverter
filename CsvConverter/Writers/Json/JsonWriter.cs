using Common.Models;
using CsvConverter.Interfaces;

namespace CsvConverter.Writers.Json;

public class JsonWriter : IWriter
{
    public string Type => "json";

    public Task WriteAsync(string outputFilename, IEnumerable<PersonDetail> peopleDetails)
    {
        throw new NotImplementedException();
    }
}