using System.Text.Json;

namespace CsvConverter.Json.Writers;

public interface IJsonSerializationService
{
    Task Serialize<T>(Stream outputStream, T data);
}

public class JsonSerializationService : IJsonSerializationService
{
    public async Task Serialize<T>(Stream outputStream, T data)
    {
        await JsonSerializer.SerializeAsync(outputStream,
                                            data,
                                            new JsonSerializerOptions
                                            {
                                                WriteIndented = true
                                            });
    }
}