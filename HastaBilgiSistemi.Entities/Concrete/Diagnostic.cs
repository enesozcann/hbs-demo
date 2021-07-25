using HastaBilgiSistemi.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Concrete
{
    public class Diagnostic : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
