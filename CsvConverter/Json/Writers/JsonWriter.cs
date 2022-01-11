using Common.Models;
using CsvConverter.Interfaces;

namespace CsvConverter.Json.Writers;

public class JsonWriter : IWriter
{
    public string Type => "json";

    public Task WriteAsync(string outputFilename, IEnumerable<PersonDetail> peopleDetails)
    {
        throw new NotImplementedException();
    }
}