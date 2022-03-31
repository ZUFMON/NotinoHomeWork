using Nutino.HomeWork.Contracts.Interfaces;
using Nutino.HomeWork.Domain.ConvertFormat;

namespace Nutino.HomeWork.Domain.Services;

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

