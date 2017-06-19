using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UsersDiosna.CMS.Models
{
    public class AddArticle
    {
        /// <summary>
        /// Model for add project status informations
        /// </summary>

        [DataType(DataType.Text)]
        [Display(Name = "For bakery (id - integer)")]
        public string bakeryId { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Subject")]
        public string Header { get; set; }

        [Required(ErrorMessage = "Project info is required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Project info status")]
        public string Text { get; set; }
    }

    public class AddSection
    {
        /// <summary>
        /// Model for add section into CMS system
        /// </summary>

        [Required(ErrorMessage = "Name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        
        public IEnumerable<SelectList> Ids { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "For bakery (id - integer)")]
        public int BakeryId { get; set; }

        [Required(ErrorMessage ="Role for this section is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Role for this section")]
        public string Role { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description of the section")]
        public string Description { get; set; }
    }
}