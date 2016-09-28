using System.Collections.Generic;

namespace ServiceEntities
{
    public class Priority
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<ServiceTask> Tasks { get; set; }

        public Priority()
        {
            Tasks = new List<ServiceTask>();
        }
    }
}
