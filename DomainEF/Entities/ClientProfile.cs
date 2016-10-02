using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
