using Common.Models;

namespace CsvConverter.Interfaces;

public interface IWriter
{
    string WriterType { get; }
    Task WriteAsync(string outputFilename, IEnumerable<PersonDetail> peopleDetails);
}