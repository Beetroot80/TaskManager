using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceEntities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public ICollection<ServiceTask> Tasks { get; set; }
        public ICollection<ApplicationUser> Clients { get; set; }

        public Project()
        {
            Tasks = new List<ServiceTask>();
            Clients = new List<ApplicationUser>();
        }
    }
}
