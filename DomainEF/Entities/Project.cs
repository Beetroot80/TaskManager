using System.Collections.Generic;

namespace DomainCore
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //Navigation properties
        public ICollection<DomainTask> Tasks { get; set; }
        public ICollection<ApplicationUser> Clients { get; set; }

        public Project()
        {
            Tasks = new List<DomainTask>();
            Clients= new List<ApplicationUser>();
        }
    }
}
