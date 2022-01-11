using System.Collections.Generic;
using CsvConverter.Interfaces;
using CsvConverter.Repositories;
using CsvConverter.Repositories.Initializers;
using Moq;
using NUnit.Framework;

namespace CsvConverter.Tests.Repositories.Initializers;

[TestFixture]
public class ReaderRepositoryInitializerTests : MockBase<ReaderRepositoryInitializer>
{
    private readonly List<IReader> _readers = new();

    protected override void Setup()
    {
        Mocker.Use<IEnumerable<IReader>>(_readers);

        base.Setup();
    }

    [Test]
    public void InitializeAll_AddsAllReadersToReaderRepository()
    {
        // Arrange
        _readers.AddRange(new[]
                          {
                              Mock.Of<IReader>(),
                              Mock.Of<IReader>(),
                              Mock.Of<IReader>()
                          });

        // Act
        Subject.InitializeAll();

        // Assert
        GetMock<IReaderRepository>()
            .Verify(x => x.Add(It.IsAny<IReader>()),
                    Times.Exactly(_readers.Count));
    }
}