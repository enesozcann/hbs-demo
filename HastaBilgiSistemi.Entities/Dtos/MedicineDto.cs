using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Dtos
{
    public class MedicineDto : DtoGetBase
    {
        public Medicine Medicine { get; set; }
    }
}
