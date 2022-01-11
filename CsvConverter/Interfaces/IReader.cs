using Common.Models;

namespace CsvConverter.Interfaces;

public interface IReader
{
    string ReaderType { get; }

    Task<IEnumerable<PersonDetail>> ReadAsync(string inputLocation);
}