using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class AddUserModel
    {
        [Required(ErrorMessage = "Field is required")]
        [EmailAddress(ErrorMessage = "Should be of type : email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [MinLength(6, ErrorMessage = "Length of this field should be not less than 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords not match")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [MaxLength(35, ErrorMessage = "Length of this field should be less than 25 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Role { get; set; }
        [MaxLength(60)]
        public string Surname { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime? BirthDate { get; set; }
    }
}