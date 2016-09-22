using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceEntities
{
    public class ClientProfile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> PhoneNumber { get; set; }
        public string Position { get; set; }
        public byte[] Photo { get; set; }
        public DateTime? BirthDate { get; set; }

        //public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
