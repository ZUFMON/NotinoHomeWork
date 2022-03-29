using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Nutino.HomeWork.Domain.XmlTemplates
{
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
}
