using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scrutor;

namespace CsvConverter;

internal class Program
{
    public static async Task<int> Main(string[] args)
    {
        return await CreateHostBuilder(args)
                   .RunCommandLineApplicationAsync<Bootstrapper>(args);
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                   .ConfigureServices((_, services) =>
                   {
                       services.AddSingleton<Bootstrapper>();
                       services.AddSingleton<IFileSystem, FileSystem>();

                       services.Scan(scan => scan.FromAssemblyOf<Program>()
                                                 .AddClasses()
                                                 .UsingRegistrationStrategy(RegistrationStrategy.Append)
                                                 .AsImplementedInterfaces()
                                                 .WithSingletonLifetime());
                   });
    }
}