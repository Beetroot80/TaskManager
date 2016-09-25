using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class TaskModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
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

        public string StatusTitle { get; set; }
        public string PriorityTitle { get; set; }
        public string AssignedToEmail { get; set; }
        public string CreatedByEmail { get; set; }
        public string ProjectTitle { get; set; }
        public CommentModel Comments { get; set; }
    }
}