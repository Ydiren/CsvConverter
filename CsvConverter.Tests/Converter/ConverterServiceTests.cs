using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using CsvConverter.Converter;
using CsvConverter.Interfaces;
using CsvConverter.Repositories;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CsvConverter.Tests.Converter;

[TestFixture]
internal class ConverterServiceTests : MockBase<ConverterService>
{
    [Test]
    public async Task ConvertAsync_WhenInputTypeIsNotSupported_ThrowsArgumentException()
    {
        // Arrange
        GetMock<IReaderRepository>()
            .Setup(x => x.Get(It.IsAny<string>()))
            .Throws<InvalidOperationException>();

        var parameters = new ConverterParameters("input.file",
                                                 "pdf",
                                                 "output.json",
                                                 "json");

        // Act
        Func<Task> convertAsync = async () => await Subject.ConvertAsync(parameters);

        // Assert
        await convertAsync.Should()
                          .ThrowAsync<InvalidOperationException>();
        GetMock<IReaderRepository>()
            .Verify(x => x.Get(parameters.InputType),
                    Times.Once);
    }

    [Test]
    public async Task ConvertAsync_WhenOutputTypeIsNotSupported_ThrowsArgumentException()
    {
        // Arrange
        GetMock<IReaderRepository>()
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(Mock.Of<IReader>());
        GetMock<IWriterRepository>()
            .Setup(x => x.Get(It.IsAny<string>()))
            .Throws<InvalidOperationException>();

        var parameters = new ConverterParameters("input.file",
                                                 "pdf",
                                                 "output.json",
                                                 "json");

        // Act
        Func<Task> convertAsync = async () => await Subject.ConvertAsync(parameters);

        // Assert
        await convertAsync.Should()
                          .ThrowAsync<InvalidOperationException>();
        GetMock<IWriterRepository>()
            .Verify(x => x.Get(parameters.OutputType),
                    Times.Once);
    }

    [Test]
    public async Task ConvertAsync_WhenBothInputAndOutputTypesAreSupported_ReadsData()
    {
        // Arrange
        var reader = GetMock<IReader>();
        GetMock<IReaderRepository>()
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(reader.Object);
        GetMock<IWriterRepository>()
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(Mock.Of<IWriter>());

        var parameters = new ConverterParameters("input.csv",
                                                 "csv",
                                                 "output.json",
                                                 "json");

        // Act
        await Subject.ConvertAsync(parameters);

        // Assert
        GetMock<IReaderRepository>()
            .Verify(x => x.Get(parameters.InputType),
                    Times.Once);
        GetMock<IWriterRepository>()
            .Verify(x => x.Get(parameters.OutputType),
                    Times.Once);
        reader.Verify(x => x.ReadAsync(parameters.Input),
                      Times.Once);
    }

    [Test]
    public async Task ConvertAsync_WhenBothInputAndOutputTypesAreSupported_PassesReadDataToWriter()
    {
        // Arrange
        var reader = GetMock<IReader>();
        var peopleDetails = new List<PersonDetail>();
        reader.Setup(x => x.ReadAsync(It.IsAny<string>()))
              .ReturnsAsync(peopleDetails);
        GetMock<IReaderRepository>()
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(reader.Object);
        var writer = GetMock<IWriter>();
        GetMock<IWriterRepository>()
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(writer.Object);

        var parameters = new ConverterParameters("input.csv",
                                                 "csv",
                                                 "output.json",
                                                 "json");

        // Act
        await Subject.ConvertAsync(parameters);

        // Assert
        GetMock<IReaderRepository>()
            .Verify(x => x.Get(parameters.InputType),
                    Times.Once);
        GetMock<IWriterRepository>()
            .Verify(x => x.Get(parameters.OutputType),
                    Times.Once);
        writer.Verify(x => x.WriteAsync(parameters.Output,
                                        peopleDetails),
                      Times.Once);
    }
}