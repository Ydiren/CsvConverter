using System;
using CsvConverter.Readers;
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
}