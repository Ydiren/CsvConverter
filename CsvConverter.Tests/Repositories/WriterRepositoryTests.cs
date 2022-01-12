using System;
using CsvConverter.Interfaces;
using CsvConverter.Repositories;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CsvConverter.Tests.Repositories;

[TestFixture]
public class WriterRepositoryTests : MockBase<WriterRepository>
{
    [Test]
    public void Add_WhenArgumentIsNull_ThrowsArgumentNullException()
    {
        // Act
        var add = () => Subject.Add(null);

        // Assert
        add.Should()
           .Throw<ArgumentNullException>();
    }

    [Test]
    public void Get_WhenTypeIsSupported_ReturnsExpectedWriterInstance()
    {
        // Arrange
        var expectedWriter = Mock.Of<IWriter>(x => x.WriterType == "csv");
        Subject.Add(expectedWriter);

        // Act
        var writer = Subject.Get("csv");

        // Assert
        writer.Should()
              .Be(expectedWriter);
    }

    [Test]
    public void Get_WhenTypeNotSupported_ThrowsInvalidOperationException()
    {
        // Act
        Action get = () => Subject.Get("unsupportedType");

        // Assert
        get.Should()
           .Throw<InvalidOperationException>();
    }

    [Test]
    public void SupportedTypes_WhenNoReadersAdded_ReturnsEmptyCollection()
    {
        // Assert
        Subject.SupportedTypes.Should()
               .BeEmpty();
    }

    [Test]
    public void SupportedTypes_WhenSingleReadersAdded_ReturnsCollectionWithSingleEntry()
    {
        // Arrange
        var writer = Mock.Of<IWriter>(x => x.WriterType == "abc");
        Subject.Add(writer);
        
        // Assert
        Subject.SupportedTypes.Should()
               .BeEquivalentTo(writer.WriterType);
    }
    
    [Test]
    public void SupportedTypes_WhenMultipleReadersAdded_ReturnsCollectionWithEntryForEachReader()
    {
        // Arrange
        var writer1 = Mock.Of<IWriter>(x => x.WriterType == "abc");
        var writer2 = Mock.Of<IWriter>(x => x.WriterType == "def");
        var writer3 = Mock.Of<IWriter>(x => x.WriterType == "123");
        Subject.Add(writer1);
        Subject.Add(writer2);
        Subject.Add(writer3);
        
        // Assert
        Subject.SupportedTypes.Should()
               .BeEquivalentTo(writer1.WriterType,
                               writer2.WriterType,
                               writer3.WriterType);
    }
}