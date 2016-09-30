using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class StatusModel
    {
        [Required]
        [MaxLength(25)]
        public string Title { get; set; }
    }
}