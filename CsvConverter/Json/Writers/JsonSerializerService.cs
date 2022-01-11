using System.Text.Json;

namespace CsvConverter.Json.Writers;

public interface IJsonSerializerService
{
    Task Serialize<T>(Stream outputStream, T data);
}

public class JsonSerializerService : IJsonSerializerService
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