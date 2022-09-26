namespace Notino.HomeWork.Contracts.Interfaces;

public interface IConvertorContentFormatFile
{
    string ConvertXmlToJson<T>(string xml) where T : new();
}

