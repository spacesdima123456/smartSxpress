using System;
using System.Xml.Serialization;

namespace WpfApp3.Models
{
    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot("item", Namespace = "", IsNullable = false)]
    public class Xml
    {
        [XmlElement("version")]
        public string Version { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

    }
}
