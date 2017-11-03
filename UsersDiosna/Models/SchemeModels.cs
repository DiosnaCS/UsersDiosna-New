using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Svg;
using System.Drawing;
using System.Xml.Serialization;

namespace UsersDiosna.Sheme.Models
{
    public class SchemeEditor
    {
        public string relativePath { get; set; }
        /// <summary>
        /// Svg model 
        /// </summary>
        public SvgDocument SvgFile {get; set; }
    }
    public class SchemeValue
    {
        public string tableName { get; set; }
        public string columnName { get; set; }
    }
    public class ResponseValue
    {
        public string tableName { get; set; }
        public string columnName { get; set; }
        public object value { get; set; }
    }

    public class DynValue
    {
        [XmlAttribute]
        public int id { get; set; }
        [XmlAttribute]
        public string table { get; set; }
        [XmlAttribute]
        public string column { get; set; }
        [XmlAttribute]
        public int ratio { get; set; }
        [XmlAttribute]
        public int offset { get; set; }
        [XmlAttribute]
        public string unit { get; set; }
        [XmlAttribute]
        public string textColor { get; set; }
    }
    public class AgeBar
    {
        [XmlAttribute]
        public int id { get; set; }
        [XmlAttribute]
        public string table { get; set; }
        [XmlAttribute]
        public string column { get; set; }
        [XmlAttribute]
        public int maxAge { get; set; }
        [XmlAttribute]
        public string firstColor { get; set; }
        [XmlAttribute]
        public int firstLimit { get; set; }
        [XmlAttribute]
        public string secondColor { get; set; }
        [XmlAttribute]
        public int secLimit { get; set; }
        [XmlAttribute]
        public string thirdColor { get; set; }
    }
    public class TextlistItem
    {
        [XmlAttribute]
        public int index { get; set; }
        [XmlAttribute]
        public string value { get; set; }
        [XmlAttribute]
        public string bgColor { get; set; }
        [XmlAttribute]
        public string textColor { get; set; }
    }
    public class Textlist
    {
        [XmlAttribute]
        public string name { get; set; }
        public List<TextlistItem> items { get; set; }
    }
    public class GraphiclistItem
    {
        [XmlAttribute]
        public int index { get; set; }
        [XmlAttribute]
        public string path { get; set; }
    }
    public class Graphiclist
    {
        public string name { get; set; }
        public List<GraphiclistItem> items { get; set; }
    }
}