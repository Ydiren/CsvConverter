using System;
using Common.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Common.Tests;

[TestFixture]
public class FileNameTests
{
    [Test]
    public void Ctor_WhenPathIsNull_ThrowsArgumentNullException()
    {
        // Act
        Action ctor = () => new FileName(null);
        
        // Assert
        ctor.Should()
            .Throw<ArgumentNullException>();
    }
    
    [Test]
    public void Ctor_WhenPathIsEmptyString_ThrowsArgumentException()
    {
        // Act
        Action ctor = () => new FileName(string.Empty);
        
        // Assert
        ctor.Should()
            .Throw<ArgumentException>();
    }
    
    [Test]
    public void Ctor_WhenPathIsWhitespace_ThrowsArgumentException()
    {
        // Act
        Action ctor = () => new FileName("    ");
        
        // Assert
        ctor.Should()
            .Throw<ArgumentException>();
    }
    
    [Test]
    public void FullPath_WhenPathStartsWithTilde_ReplacesTildeWithUserProfileDirectory()
    {
        // Arrange
        var filepath = "~/bogus.csv";
            
        // Act
        var filename = new FileName(filepath);
        
        // Assert
        filename.FullPath.Should()
            .NotContain("~");
        filename.FullPath.Should()
            .StartWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
    }
    
    [Test]
    public void ToString_ReturnsFullPathProperty()
    {
        // Arrange
        var filepath = "/bogus.csv";
            
        // Act
        var filename = new FileName(filepath);
        
        // Assert
        filename.FullPath.Should()
            .Be(filename.ToString());
    }
}