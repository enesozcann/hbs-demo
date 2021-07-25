using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Shared.Entities.Abstract;
using HastaBilgiSistemi.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Dtos
{
    public class HospitalDto : DtoGetBase
    {
        public Hospital Hospital { get; set; }
    }
}
