using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Web.Mvc;
using UsersDiosna.Models;

namespace UsersDiosna.CMS.Models
{
    public class ArticleModel
    {
        /// <summary>
        /// Model for add article
        /// </summary>
        public List<SelectListItem> Ids { get; set; }

        [Required(ErrorMessage = "Bakery is required")]
        [DataType(DataType.Text)]
        [Display(Name = "For bakery")]
        public int bakeryId { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Subject")]
        public string Header { get; set; }

        [AllowHtml]
        [Display(Name = "Text of this article")]
        public string Text { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Amount of money")]
        public string Amount { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Hours spent on vice work")]
        public int? HoursSpend { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Attachment")]
        public string Attachment { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description of the work which has been done")]
        public string Description { get; set; }

        public List<SelectListItem> Sections { get; set; }

        [Required(ErrorMessage = "Section is required")]
        [DataType(DataType.Text)]
        [Display(Name = "In Section")]
        public int SectionId { get; set; }
    }

    public class SectionModel
    {
        /// <summary>
        /// Model for add section into CMS system
        /// </summary>

        [Required(ErrorMessage = "Name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        public List<SelectListItem> Ids { get; set; }

        [Required(ErrorMessage = "Bakery is required")]
        [DataType(DataType.Text)]
        [Display(Name = "For bakery")]
        public int BakeryId { get; set; }

        public List<SelectListItem> Roles { get; set; }

        [Required(ErrorMessage ="Role for this section is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Role for this section")]
        public string Role { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description of the section")]
        public string Description { get; set; }
    }

    public class SectionJSON {

        public string Name;

        public string Description;

        public long? ArticleId;

        public string Role;

        public int BakeryId;

    }
}