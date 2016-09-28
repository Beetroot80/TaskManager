using System.Collections.Generic;

namespace ServiceEntities
{
    public class Status
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<ServiceTask> Tasks { get; set; }

        public Status()
        {
            Tasks = new List<ServiceTask>();
        }
    }
}
