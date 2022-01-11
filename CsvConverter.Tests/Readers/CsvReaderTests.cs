using System;
using CsvConverter.Readers;
using FluentAssertions;
using NUnit.Framework;

namespace CsvConverter.Tests.Readers;

[TestFixture]
public class CsvReaderTests : MockBase<CsvReader>
{
    [Test]
    public void Read_WhenInputIsEmpty_ThrowsArgumentException()
    {
        // Act
        Action read = () => Subject.Read(string.Empty);
        
        // Assert
        read.Should()
            .Throw<ArgumentException>();
    }
}