using Common.Models;

namespace CsvConverter.Interfaces;

public interface IWriter
{
    string Type { get; }
    Task WriteAsync(string outputFilename, IEnumerable<PersonDetail> peopleDetails);
}