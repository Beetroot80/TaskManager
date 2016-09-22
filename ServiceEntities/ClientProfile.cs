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
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public DateTime? BirthDate { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
