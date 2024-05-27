using System.ComponentModel.DataAnnotations;

namespace clinicmanagement.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Specialisation { get; set; }
    }
}
