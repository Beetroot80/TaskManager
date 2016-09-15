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

        public ICollection<ServiceTask> Tasks { get; set; }
        public ICollection<User> Users { get; set; }

        public Project()
        {
            Tasks = new List<ServiceTask>();
            Users = new List<User>();
        }
    }
}
