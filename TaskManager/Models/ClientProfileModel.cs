using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class ClientProfileModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Surname { get; set; }
        [DataType(DataType.PhoneNumber)]
        public List<string> PhoneNumber { get; set; }
        public string Position { get; set; }
        public byte[] Photo { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
    }
}