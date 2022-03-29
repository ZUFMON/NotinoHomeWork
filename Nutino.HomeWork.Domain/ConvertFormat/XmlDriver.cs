using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Nutino.HomeWork.Contracts.Interfaces;

namespace Nutino.HomeWork.Domain.ConvertFormat;

    public class XmlDriver
    {
        public T LoadXmlToObject<T>(string xml) where T : new()
        {
            //TODO OT: bylo by dobre mit validaci XML dle XSD
            if (string.IsNullOrEmpty(xml)) throw new FormatException("Deserialization XML is not possible. String data is null or empty!");
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using StringReader reader = new StringReader(xml);

            object test = (T)serializer.Deserialize(reader);
            if (test == null) throw new InvalidDataException("Deserialization Xml template return null value");

            return (T)test;
        }
    }