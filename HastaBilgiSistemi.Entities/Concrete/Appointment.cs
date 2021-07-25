using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HastaBilgiSistemi.Shared.Entities.Abstract;

namespace HastaBilgiSistemi.Entities.Concrete
{
    public class Appointment : EntityBase, IEntity
    {
        public DateTime AppointmentDate { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<Diagnostic> Diagnostics { get; set; }
    }
}
