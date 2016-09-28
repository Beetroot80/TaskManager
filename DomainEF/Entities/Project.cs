using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }

        public virtual ICollection<DomainTask> Tasks { get; set; }
        public virtual ICollection<ApplicationUser> Clients { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }

        public Project()
        {
            Tasks = new List<DomainTask>();
            Clients= new List<ApplicationUser>();
        }
    }
}
