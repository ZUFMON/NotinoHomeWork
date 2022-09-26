using System.Xml.Serialization;

namespace Notino.HomeWork.Domain.XmlTemplates;

    /// <remarks/>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "Document", Namespace = "", IsNullable = false)]
    public partial class DocumentXML
    {
        [XmlElement(ElementName = "Title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "Text")]
        public string Text { get; set; }
    }

