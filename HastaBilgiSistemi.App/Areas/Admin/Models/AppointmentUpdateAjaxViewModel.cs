using HastaBilgiSistemi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Areas.Admin.Models
{
    public class AppointmentUpdateAjaxViewModel
    {
        public AppointmentUpdateDto  AppointmentUpdate { get; set; }
        public string AppointmentUpdatePartial { get; set; }
        public AppointmentDto AppointmentDto { get; set; }
    }
}
