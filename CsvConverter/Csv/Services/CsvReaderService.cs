using System.Globalization;
using Common.Models;
using CsvHelper;

namespace CsvConverter.Csv.Services;

public interface ICsvReaderService
{
    Task<IEnumerable<TRecord>> ReadAsync<TRecord>(FileName filename) where TRecord : class;
}

public class CsvReaderService : ICsvReaderService
{
    public async Task<IEnumerable<TRecord>> ReadAsync<TRecord>(FileName filename) where TRecord : class
    {
        List<TRecord> records = new();
        using var reader = new StreamReader(filename.FullPath);
        using var csvReader = new CsvReader(reader,
                                            CultureInfo.InvariantCulture);
        await foreach (var record in csvReader.GetRecordsAsync<TRecord>()) records.Add(record);

        return records;
    }
}