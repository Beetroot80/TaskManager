using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class ViewTasksModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte? Percentage { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeadLine { get; set; }

        public int ProjectId { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
        public string AssignedToId { get; set; }
        public string CreatedById { get; set; }

        public string StatusTitle { get; set; }
        public string PriorityTitle { get; set; }
        public string AssignedToEmail { get; set; }
        public string CreatedByEmail { get; set; }
    }
}