using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Models
{
    public class PatientHomeViewModel
    {
        public AppointmentListDto LastAppointment { get; set; }
        public AppointmentListDto AppointmentListDto { get; set; }
    }
}
