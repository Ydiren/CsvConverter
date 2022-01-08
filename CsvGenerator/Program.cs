using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CsvGenerator;

internal class Program
{
    public static Task<int> Main(string[] args)
    {
        return CreateHostBuilder(args)
            .RunCommandLineApplicationAsync<CsvGenerator>(args);
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<IRowGenerator, RowGenerator>();
            });
    }
}