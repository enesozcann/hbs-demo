using HastaBilgiSistemi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Areas.Admin.Models
{
    public class HospitalAddAjaxViewModel
    {
        public HospitalAddDto  HospitalAdd { get; set; }
        public string HospitalAddPartial { get; set; }
        public HospitalDto HospitalDto { get; set; }
    }
}
