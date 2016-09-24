using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DomainCore
{
    public class ApplicationUser : IdentityUser
    {
        public string ClientProfileId { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }

        public IEnumerable<string> UserRoles { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<DomainTask> DomainTasks { get; set; }

        public ApplicationUser()
        {
            Comments = new List<Comment>();
            Projects = new List<Project>();
            DomainTasks = new List<DomainTask>();
        }
    }
}
