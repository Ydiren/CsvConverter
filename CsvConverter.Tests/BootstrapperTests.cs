using System.Collections.Generic;
using System.Threading.Tasks;
using CsvConverter.Repositories.Initializers;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.HelpText;
using Moq;
using NUnit.Framework;

namespace CsvConverter.Tests;

internal class BootstrapperTests : MockBase<Bootstrapper>
{
    private IRepositoryInitializer[] _repositoryInitializers = { };

    [SetUp]
    protected override void Setup()
    {
        var repositoryInitializer = GetMock<IRepositoryInitializer>().Object;
        _repositoryInitializers = new[]
        {
            repositoryInitializer,
            repositoryInitializer,
            repositoryInitializer
        };
        Mocker.Use<IEnumerable<IRepositoryInitializer>>(_repositoryInitializers);

        Mocker.Use<CommandLineApplication>(new CommandLineApplication<Bootstrapper>(Mock.Of<IHelpTextGenerator>(), Mock.Of<IConsole>(), System.IO.Directory.GetCurrentDirectory()));
    }

    [Test]
    public async Task OnExecuteAsync_RegistersReadersAndWriters()
    {
        // Act
        await Subject.OnExecuteAsync();
        
        // Assert
        GetMock<IRepositoryInitializer>()
            .Verify(x => x.InitializeAll(), Times.Exactly(_repositoryInitializers.Length));
    }
}