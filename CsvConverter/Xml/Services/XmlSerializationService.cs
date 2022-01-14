using System.Xml.Serialization;

namespace CsvConverter.Xml.Services;

public interface IXmlSerializationService
{
    void Serialize<T>(Stream outputStream, T data);
}

internal class XmlSerializationService : IXmlSerializationService
{
    public void Serialize<T>(Stream outputStream, T data)
    {
        var serializer = new XmlSerializer(typeof(T));
        serializer.Serialize(outputStream, data);
    }
}