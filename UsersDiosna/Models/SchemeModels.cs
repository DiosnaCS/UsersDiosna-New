using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Svg;

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
}