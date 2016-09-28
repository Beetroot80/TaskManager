using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities
{
    public class DomainTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeadLine { get; set; }

        public int ProjectId { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
        [ForeignKey("Client")]
        public string AssignedTo { get; set; }
        [ForeignKey("CreatedBy")]
        public string CreatedBy_Id { get; set; }

        public virtual Project Project { get; set; }
        public virtual Status Status { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual ApplicationUser Client { get; set; } //Task asigned to client
        public virtual ApplicationUser CreatedBy { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public DomainTask()
        {
            Comments = new List<Comment>();
        }
    }
}
