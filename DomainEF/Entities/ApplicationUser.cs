using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DomainEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string ClientProfileId { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }

        public ICollection<string> UserRoles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<DomainTask> DomainTasks { get; set; }

        public ApplicationUser()
        {
            Comments = new List<Comment>();
            Projects = new List<Project>();
            DomainTasks = new List<DomainTask>();
        }
    }
}
