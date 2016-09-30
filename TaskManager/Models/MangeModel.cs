using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class ManageModel
    {
        [Required]
        [MaxLength(25)]
        public string ProjectTitle { get; set; }

        [Required]
        [MaxLength(25)]
        public string TaskTitle { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string AssignToEmail { get; set; }

        public List<string> ProjectActions { get; set; }
        public List<string> TaskAction { get; set; }
    }
}