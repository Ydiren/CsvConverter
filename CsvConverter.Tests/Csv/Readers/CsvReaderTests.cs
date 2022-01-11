using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Common.Models;
using CsvConverter.Csv.Factories;
using CsvConverter.Csv.Readers;
using CsvConverter.Csv.Services;
using CsvConverter.Tests.MockData;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CsvConverter.Tests.Csv.Readers;

[TestFixture]
public class CsvReaderTests : MockBase<CsvReader>
{
    protected override void Setup()
    {
        Mocker.Use<IPersonDetailsFactory>(new PersonDetailsFactory());
        base.Setup();
    }

    [Test]
    public async Task ReadAsync_WhenInputIsEmpty_ThrowsArgumentException()
    {
        // Act
        Func<Task> read = async () => await Subject.ReadAsync(string.Empty);

        // Assert
        await read.Should()
                  .ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task ReadAsync_WhenInputIsAllWhitespace_ThrowsArgumentException()
    {
        // Act
        Func<Task> read = async () => await Subject.ReadAsync("    ");

        // Assert
        await read.Should()
                  .ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task ReadAsync_WhenInputIsInvlidFilename_ThrowsArgumentException()
    {
        var invalidPathChars = new string(Path.GetInvalidPathChars());

        // Act
        Func<Task> read = async () => await Subject.ReadAsync(invalidPathChars);

        // Assert
        await read.Should()
                  .ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task ReadAsync_WhenFileContainsNoRecords_ReturnsEmptyEnumerable()
    {
        // Arrange
        GetMock<ICsvReaderService>()
            .Setup(x => x.ReadAsync<CsvRow>(It.IsAny<FileName>()))
            .ReturnsAsync(new List<CsvRow>());

        // Act
        var result = await Subject.ReadAsync("input.csv");

        // Assert
        result.Should()
              .BeEmpty();
    }

    [Test]
    public async Task ReadAsync_WhenCsvReaderServiceThrowsException_ReturnsEmptyEnumerable()
    {
        // Arrange
        GetMock<ICsvReaderService>()
            .Setup(x => x.ReadAsync<CsvRow>(It.IsAny<FileName>()))
            .Throws<ArgumentNullException>();

        // Act
        var result = await Subject.ReadAsync("input.csv");

        // Assert
        result.Should()
              .BeEmpty();
    }

    [Test]
    public async Task ReadAsync_WhenCsvReaderServiceReturnsValidRows_ReturnsConvertedRows()
    {
        // Arrange
        var mockCustomerRows = new MockDataGenerator().GenerateCustomerCsvRows(3);
        GetMock<ICsvReaderService>()
            .Setup(x => x.ReadAsync<CsvRow>(It.IsAny<FileName>()))
            .ReturnsAsync(mockCustomerRows);

        // Act
        var result = await Subject.ReadAsync("input.csv");

        // Assert
        result.Should()
              .HaveCount(3);
    }
}