using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Security;
using System.Threading.Tasks;
using Common.Models;
using CsvConverter.Json.Writers;
using CsvConverter.Tests.MockData;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CsvConverter.Tests.Json.Writers;

[TestFixture]
internal class JsonWriterTests : MockBase<JsonWriter>
{
    private Mock<IFileSystem> _mockFileSystem = null!;
    private MemoryStream _fakeFileStream = null!;

    protected override void Setup()
    {
        _fakeFileStream = new MemoryStream();
        var mockFileStreamFactory = new Mock<IFileStreamFactory>();
        mockFileStreamFactory.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<FileMode>()))
                             .Returns(_fakeFileStream);
        
        _mockFileSystem = new Mock<IFileSystem>();
        _mockFileSystem.SetupGet(x => x.FileStream)
                       .Returns(mockFileStreamFactory.Object);
        
        Mocker.Use(_mockFileSystem.Object);
        
        base.Setup();
    }

    [TearDown]
    public void TestTearDown()
    {
        _fakeFileStream.Dispose();
    }

    [Test]
    public void WriterType_ReturnsJson()
    {
        // Assert
        Subject.WriterType.Should()
               .Be("json");
    }
    [Test]
    public async Task WriteAsync_WhenOutputFilenameIsInvalid_ThrowsArgumentException()
    {
        // Act
        Func<Task> writeAsync = async () => await Subject.WriteAsync("",
                                                                     new List<PersonDetail>());
        
        // Assert
        await writeAsync.Should()
                        .ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task WriteAsync_WhenUserDoesntHaveAccessToOutputFilename_DoesNotThrow()
    {
        // Arrange
        var outputFilename = @"c:\json\output.json";
        var peopleDetails = new MockDataGenerator().GeneratePeopleDetails(3);
        _mockFileSystem.Setup(x => x.FileStream.Create(outputFilename, FileMode.Create))
                       .Throws<SecurityException>();

        // Act
        Func<Task> writeAsync = async () => await Subject.WriteAsync(outputFilename,
                                 peopleDetails);

        // Assert
        await writeAsync.Should()
                        .NotThrowAsync<SecurityException>();
    }

    [Test]
    public async Task WriteAsync_WhenOutputFileIsReadOnly_DoesNotThrow()
    {
        // Arrange
        var outputFilename = @"c:\json\output.json";
        var peopleDetails = new MockDataGenerator().GeneratePeopleDetails(3);
        _mockFileSystem.Setup(x => x.FileStream.Create(outputFilename, FileMode.Create))
                       .Throws<UnauthorizedAccessException>();

        // Act
        Func<Task> writeAsync = async () => await Subject.WriteAsync(outputFilename,
                                 peopleDetails);

        // Assert
        await writeAsync.Should()
                        .NotThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task WriteAsync_WhenOutputFileIsValidAndUserHasAccess_SerializesObjectToJsonFile()
    {
        // Arrange
        var outputFilename = @"c:\json\output.json";
        var peopleDetails = new MockDataGenerator().GeneratePeopleDetails(3);

        // Act
         await Subject.WriteAsync(outputFilename,
                                 peopleDetails);

        // Assert
        GetMock<IJsonSerializationService>()
            .Verify(x => x.Serialize(_fakeFileStream, peopleDetails), Times.Once);
    }
}