using HastaBilgiSistemi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Areas.Admin.Models
{
    public class HospitalUpdateAjaxViewModel
    {
        public HospitalUpdateDto  HospitalUpdate { get; set; }
        public string HospitalUpdatePartial { get; set; }
        public HospitalDto HospitalDto { get; set; }
    }
}
