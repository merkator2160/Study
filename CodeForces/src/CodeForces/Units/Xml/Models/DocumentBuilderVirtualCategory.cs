using System;
using System.Xml.Serialization;

namespace CodeForces.Units.Xml.Models
{
    [Serializable]
    public class DocumentBuilderVirtualCategory
    {
        /// <summary>
        /// Id родительской категории
        /// </summary>
        [XmlIgnore]
        public Int32? ParentId { get; set; }
        /// <summary>
        /// Id категории
        /// </summary>
        [XmlAttribute]
        public Int64 Id { get; set; }
        /// <summary>
        /// Имя родительской категории
        /// </summary>
        [XmlAttribute]
        public String Name { get; set; }

        [XmlAttribute("ParentId")]
        public String ParentIdForSerialization
        {
            get { return ParentId?.ToString(); }
            set { }
        }

        [XmlIgnore]
        public String SeoId { get; set; }
    }
}