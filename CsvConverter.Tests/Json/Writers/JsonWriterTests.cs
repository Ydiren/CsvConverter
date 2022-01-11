using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Threading.Tasks;
using Common.Models;
using CsvConverter.Json.Writers;
using CsvConverter.Tests.MockData;
using FluentAssertions;
using NUnit.Framework;

namespace CsvConverter.Tests.Json.Writers;

[TestFixture]
public class JsonWriterTests : MockBase<JsonWriter>
{
    private MockFileSystem _mockFileSystem = null!;

    protected override void Setup()
    {
        var directoryStructure = new Dictionary<string, MockFileData>
                                 {
                                     { @"\json", new MockFileData("{}") }
                                 };
        _mockFileSystem = new MockFileSystem(directoryStructure);
        Mocker.Use<IFileSystem>(_mockFileSystem);
        
        base.Setup();
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
    public async Task WriteAsync_WhenOutputFilenameIsValid_CreatesFileAndWritesJson()
    {
        // Arrange
        var peopleDetails = new MockDataGenerator().GeneratePeopleDetails(3);
        
        // Act
        await Subject.WriteAsync(@"\json\output.json",
                                 peopleDetails);

        // Assert
        _mockFileSystem.File.Exists(@"\json\output.json")
                       .Should()
                       .BeTrue();
    }
}