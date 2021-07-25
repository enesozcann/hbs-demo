using HastaBilgiSistemi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Areas.Admin.Models
{
    public class AppointmentAddAjaxViewModel
    {
        public AppointmentAddDto  AppointmentAdd { get; set; }
        public string AppointmentAddPartial { get; set; }
        public AppointmentDto AppointmentDto { get; set; }
    }
}
