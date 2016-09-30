using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class PriorityModel
    {
        [Required]
        [MaxLength(25)]
        public string Title { get; set; }
    }
}