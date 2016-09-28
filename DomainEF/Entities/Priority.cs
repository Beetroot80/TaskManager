using System.Collections.Generic;

namespace DomainEntities
{
    public class Priority
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<DomainTask> Tasks { get; set; }

        public Priority()
        {
            Tasks = new List<DomainTask>();
        }
    }
}
