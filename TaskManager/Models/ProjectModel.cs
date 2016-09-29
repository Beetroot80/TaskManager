using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class ProjectModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(25, ErrorMessage = "Length of this field should be less than 25 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(255, ErrorMessage = "Length of this field should be less than 255 characters")]
        public string Description { get; set; }

        public int? ClientsCount { get; set; }
        public int? TaskCount { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string CreatedById { get; set; }
    }
}