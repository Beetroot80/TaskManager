using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceEntities;

namespace TaskManager.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public int? ClientsCount { get; set; }
        public int? TaskCount { get; set; }
    }
}