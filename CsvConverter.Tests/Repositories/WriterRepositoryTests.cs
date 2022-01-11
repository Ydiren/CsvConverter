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
        var expectedWriter = Mock.Of<IWriter>(x => x.Type == "csv");
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
}