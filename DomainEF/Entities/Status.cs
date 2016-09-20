using System.Collections.Generic;

namespace DomainCore
{
    public class Status
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //Foreign keys
        //Navigation properties
        public ICollection<DomainTask> DomainTasks { get; set; }

        public Status()
        {
            DomainTasks = new List<DomainTask>();
        }
    }
}
