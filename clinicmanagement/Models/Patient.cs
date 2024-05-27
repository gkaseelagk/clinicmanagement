using System.ComponentModel.DataAnnotations;

namespace clinicmanagement.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Gender { get; set; }
    }
}
