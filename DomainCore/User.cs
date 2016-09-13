using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCore
{
    public class User
    {
        public int Id { get; set; }

        //Foreign keys
        public int? PersonalInfoId { get; set; }

        //Navigation properties
        public PersonalInfo PersonalInfo { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<DomainTask> DomainTasks { get; set; }

        public User()
        {
            Comments = new List<Comment>();
            Projects = new List<Project>();
            DomainTasks = new List<DomainTask>();
        }
    }
}
