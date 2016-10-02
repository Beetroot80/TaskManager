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
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(35)]
        public string Name { get; set; }
        public string Surname { get; set; }

        [Required]
        [Display(Name ="User role")]
        public string UserRoles { get; set; }

        [Required]
        [Display (Name = "Change role")]
        public string NewRole { get; set; }

        public int SelectedItemId { get; set; }

        public SelectList RoleList { get; set; }
    }
}