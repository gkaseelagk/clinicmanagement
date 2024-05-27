using clinicmanagement.Models;
using System.ComponentModel.DataAnnotations;

namespace clinicmanagement.ViewModel
{
    public class ClinicViewModel
    {
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        //public Appointment? Appointment { get; set; }

        public List<Patient>? Patients { get; set; }
        public List<Doctor>? Doctors { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
