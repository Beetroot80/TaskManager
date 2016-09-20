using System.Collections.Generic;

namespace DomainCore
{
    public class Priority
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //Foreign keys
        //Navigation properties
        public ICollection<DomainTask> Tasks { get; set; }

        public Priority()
        {
            Tasks = new List<DomainTask>();
        }
    }
}
