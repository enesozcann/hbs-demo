using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HastaBilgiSistemi.Shared.Entities.Abstract;

namespace HastaBilgiSistemi.Entities.Concrete
{
    public class Doctor : EntityBase, IEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int PoliclinicId { get; set; }
        public Polyclinic Policlinic { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Diagnostic> Diagnostics { get; set; }
    }
}
