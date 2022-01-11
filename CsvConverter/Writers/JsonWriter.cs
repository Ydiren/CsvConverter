using Common;
using Common.Models;

namespace CsvConverter.Writers;

public class JsonWriter : IWriter
{
    public string Type { get; }
    
    public void Write(string outputFilename, IEnumerable<PersonDetail> peopleDetails)
    {
        throw new NotImplementedException();
    }
}