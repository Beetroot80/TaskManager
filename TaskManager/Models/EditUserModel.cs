using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class EditUserModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        [Display(Name ="User role")]
        public IEnumerable<string> UserRoles { get; set; }
    }
}