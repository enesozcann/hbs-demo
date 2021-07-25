using HastaBilgiSistemi.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Concrete
{
    public class Medicine : EntityBase,IEntity
    {
        public string Name { get; set; }
        public string Prospectus { get; set; }
        public string ATCName { get; set; }
        public string Company { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
