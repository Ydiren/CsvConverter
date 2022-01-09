using System.Collections.Generic;
using CsvConverter.Repositories;
using CsvConverter.Repositories.Initializers;
using CsvConverter.Writers;
using Moq;
using NUnit.Framework;

namespace CsvConverter.Tests.Repositories.Initializers;

[TestFixture]
public class WriterRepositoryInitializerTests : MockBase<WriterRepositoryInitializer>
{
    private List<IWriter> _writers = new();

    protected override void Setup()
    {
        Mocker.Use<IEnumerable<IWriter>>(_writers);
        base.Setup();
    }

    [Test]
    public void InitializeAll_AddsAllWritersToWriterRepository()
    {
        // Arrange
        _writers.AddRange(new []
        {
            Mock.Of<IWriter>(),
            Mock.Of<IWriter>(),
            Mock.Of<IWriter>()
        });
        
        // Act
        Subject.InitializeAll();
        
        // Assert
        GetMock<IWriterRepository>()
            .Verify(x => x.Add(It.IsAny<IWriter>()), Times.Exactly(_writers.Count));
    }
}