using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceEntities
{
    public class User
    {
        public int Id { get; set; }

        //Foreign keys
        public int PersonalInfoId { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<ServiceTask> DomainTasks { get; set; }

        public User()
        {
            Comments = new List<Comment>();
            Projects = new List<Project>();
            DomainTasks = new List<ServiceTask>();
        }
    }
}
