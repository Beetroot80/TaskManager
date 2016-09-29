using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class AddUserModel
    {
        [Required(ErrorMessage ="*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [MinLength(6, ErrorMessage = "Length of this field should be not less than 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords not match")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(25, ErrorMessage = "Length of this field should be less than 25 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*")]
        public string Role { get; set; }

        public string Surname { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Incorrect data type")]
        [Display(Name = "Date of birth")]
        public DateTime? BirthDate { get; set; }
    }
}