using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCore
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<DomainTask> DomainTasks { get; set; }

        public ClientProfile()
        {
            Comments = new List<Comment>();
            Projects = new List<Project>();
            DomainTasks = new List<DomainTask>();
        }
    }
}
