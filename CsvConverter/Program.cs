using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CsvConverter;

internal class Program
{
    public static Task<int> Main(string[] args)
    {
        return CreateHostBuilder(args)
            .RunCommandLineApplicationAsync<ConverterService>(args);
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<ConverterService>();
            });
    }
}