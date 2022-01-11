using Common.Models;

namespace CsvConverter.Writers;

public interface IWriter
{
    string Type { get; }
    void Write(string outputFilename, IEnumerable<PersonDetail> peopleDetails);
}