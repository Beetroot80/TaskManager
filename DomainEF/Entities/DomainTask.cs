using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainCore
{
    public class DomainTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte? Percentage { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeadLine { get; set; }

        //Foreign keys
        public int ProjectId { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
        [ForeignKey("User")]
        public int? AssignedTo { get; set; }
        [ForeignKey("CreatedBy")]
        public int CreatedBy_Id { get; set; }

        //Navigation properties
        public Project Project { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public User User { get; set; }
        public User CreatedBy { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public DomainTask()
        {
            Comments = new List<Comment>();
        }
    }
}
