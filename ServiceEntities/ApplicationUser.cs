using System;
using System.Collections.Generic;

namespace ServiceEntities
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        //public IEnumerable<string> UserRoles { get; set; }
        public string UserRoles { get; set; }

        public string ClientProfileId { get; set; }
        public ClientProfile ClientProfile { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<ServiceTask> Tasks { get; set; }

        public ApplicationUser()
        {
            Comments = new List<Comment>();
            Projects = new List<Project>();
            Tasks = new List<ServiceTask>();
        }
    }
}
