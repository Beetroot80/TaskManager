using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class ViewTasksModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public byte? Percentage { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime? DeadLine { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ProjectId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? StatusId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? PriorityId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string AssignedToId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string CreatedById { get; set; }

        [Display(Name = "Status")]
        [Required]
        public string StatusTitle { get; set; }
        [Display(Name = "Priority")]
        [Required]
        public string PriorityTitle { get; set; }
        [Display(Name = "Assign to")]
        [Required]
        public string AssignedToEmail { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string CreatedByEmail { get; set; }
        [Display(Name = "Project")]
        [Required]
        public string ProjectTitle { get; set; }

        [HiddenInput(DisplayValue = false)]
        public List<CommentModel> Comments { get; set; }
    }
}