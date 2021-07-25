using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HastaBilgiSistemi.Shared.Entities.Abstract;
namespace HastaBilgiSistemi.Entities.Concrete
{
    public class Hospital : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Polyclinic> Policlinics { get; set; }
    }
}
