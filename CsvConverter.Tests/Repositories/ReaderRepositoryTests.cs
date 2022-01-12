using System;
using CsvConverter.Interfaces;
using CsvConverter.Repositories;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CsvConverter.Tests.Repositories;

[TestFixture]
public class ReaderRepositoryTests : MockBase<ReaderRepository>
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
    public void Get_WhenTypeIsSupported_ReturnsExpectedReaderInstance()
    {
        // Arrange
        var expectedReader = Mock.Of<IReader>(x => x.ReaderType == "csv");
        Subject.Add(expectedReader);

        // Act
        var reader = Subject.Get("csv");

        // Assert
        reader.Should()
              .Be(expectedReader);
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
        var reader = Mock.Of<IReader>(x => x.ReaderType == "abc");
        Subject.Add(reader);
        
        // Assert
        Subject.SupportedTypes.Should()
               .BeEquivalentTo(reader.ReaderType);
    }
    
    [Test]
    public void SupportedTypes_WhenMultipleReadersAdded_ReturnsCollectionWithEntryForEachReader()
    {
        // Arrange
        var reader1 = Mock.Of<IReader>(x => x.ReaderType == "abc");
        var reader2 = Mock.Of<IReader>(x => x.ReaderType == "def");
        var reader3 = Mock.Of<IReader>(x => x.ReaderType == "123");
        Subject.Add(reader1);
        Subject.Add(reader2);
        Subject.Add(reader3);
        
        // Assert
        Subject.SupportedTypes.Should()
               .BeEquivalentTo(reader1.ReaderType,
                               reader2.ReaderType,
                               reader3.ReaderType);
    }
}