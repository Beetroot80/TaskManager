using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class ApplicationUserModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(35)]
        public string UserName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ClientProfileId { get; set; }
        public ClientProfileModel ClientProfile { get; set; }

        public ICollection<CommentModel> Comments { get; set; }
        public ICollection<ProjectModel> Projects { get; set; }
        public ICollection<ViewTasksModel> Tasks { get; set; }

    }
}