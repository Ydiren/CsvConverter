using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
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
public class JsonWriterTests : MockBase<JsonWriter>
{
    private Mock<IFileSystem> _mockFileSystem = null!;
    private MemoryStream _memoryStream = null!;

    protected override void Setup()
    {
        _memoryStream = new MemoryStream();
        var mockFileStreamFactory = new Mock<IFileStreamFactory>();
        mockFileStreamFactory.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<FileMode>()))
                             .Returns(_memoryStream);
        
        _mockFileSystem = new Mock<IFileSystem>();
        _mockFileSystem.SetupGet(x => x.FileStream)
                       .Returns(mockFileStreamFactory.Object);
        
        Mocker.Use(_mockFileSystem.Object);
        
        base.Setup();
    }

    [TearDown]
    public void TestTearDown()
    {
        _memoryStream.Dispose();
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
        _mockFileSystem.Setup(x => x.FileStream.Create(It.IsAny<string>(), It.IsAny<FileMode>()))
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
        _mockFileSystem.Setup(x => x.FileStream.Create(It.IsAny<string>(), It.IsAny<FileMode>()))
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
        GetMock<IJsonSerializerService>()
            .Verify(x => x.Serialize(_memoryStream, peopleDetails), Times.Once);
    }
}