using CsvConverter.Readers;
using NUnit.Framework;

namespace CsvConverter.Tests.Readers;

[TestFixture]
public class CsvReaderTests : MockBase<CsvReader>
{
    [Test]
    public void Read_WhenInputIsNull_Throws()
    {
        Assert.Fail();
    }
}