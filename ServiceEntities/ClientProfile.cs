using System;

namespace ServiceEntities
{
    public class ClientProfile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
