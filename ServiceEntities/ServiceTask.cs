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
        public string CreatedBy { get; set; }
        public string AssignedTo { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }


        public ICollection<Comment> Comments { get; set; }

        public ServiceTask()
        {
            Comments = new List<Comment>();
        }
    }
}
