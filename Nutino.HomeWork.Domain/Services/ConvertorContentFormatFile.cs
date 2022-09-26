using Notino.HomeWork.Contracts.Interfaces;
using Notino.HomeWork.Domain.ConvertFormat;

namespace Notino.HomeWork.Domain.Services;

    public class ConvertorContentFormatFile : IConvertorContentFormatFile
    {
        public string ConvertXmlToJson<T>(string xml) where T : new()
        {
            var xmlDriver = new XmlDriver();
            T obj = xmlDriver.LoadXmlToObject<T>(xml);

            var jsonDriver = new JsonDriver();
            var jsonData = jsonDriver.ObjToJson(obj);

            return jsonData;
        }
    }

