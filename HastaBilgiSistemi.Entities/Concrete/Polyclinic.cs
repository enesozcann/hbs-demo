using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HastaBilgiSistemi.Shared.Entities.Abstract;

namespace HastaBilgiSistemi.Entities.Concrete
{
    public class Polyclinic : EntityBase, IEntity
    {
        public string Name { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
