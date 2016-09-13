using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCore
{
    public class Priority
    {
        public int Id { get; set; }
        public string Description { get; set; }

        //Foreign keys
        //Navigation properties
        public ICollection<DomainTask> Tasks { get; set; }

        public Priority()
        {
            Tasks = new List<DomainTask>();
        }
    }
}
