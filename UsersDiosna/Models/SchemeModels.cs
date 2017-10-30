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