using Common.Models;

namespace CsvConverter.Readers;

public interface IReader
{
    string ReaderType { get; }
    
    IEnumerable<PersonDetail> Read(string inputLocation);
}