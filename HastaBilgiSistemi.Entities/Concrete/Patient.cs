using HastaBilgiSistemi.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Concrete
{
    public class Patient : EntityBase, IEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public long IdentityNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Address { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Diagnostic> Diagnostics { get; set; }
    }
}
