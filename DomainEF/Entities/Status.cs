using System.Collections.Generic;

namespace DomainEntities
{
    public class Status
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<DomainTask> DomainTasks { get; set; }

        public Status()
        {
            DomainTasks = new List<DomainTask>();
        }
    }
}
