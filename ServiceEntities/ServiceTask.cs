using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceEntities
{
    public class ServiceTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte? Percentage { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeadLine { get; set; }

        public int ProjectId { get; set; }
        public string CreatedById { get; set; }
        public string AssignedToId { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }


        public ICollection<Comment> Comments { get; set; }
        public Project Project { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public ApplicationUser Client { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public ServiceTask()
        {
            Comments = new List<Comment>();
        }
    }
}
