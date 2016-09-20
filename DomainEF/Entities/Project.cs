using System.Collections.Generic;

namespace DomainCore
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //Navigation properties
        public ICollection<DomainTask> DomainTasks { get; set; }
        public ICollection<User> Users { get; set; }

        public Project()
        {
            DomainTasks = new List<DomainTask>();
            Users = new List<User>();
        }
    }
}
