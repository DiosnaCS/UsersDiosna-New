using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace UsersDiosna.Download.Models
{
    public class FileForDownload
    {
        /// <summary>
        /// Model of files for download them
        /// </summary>
        public string maskName { get; set; }
        public List<string> pathes { get; set; }
        public List<string> files { get; set; }
    }
}