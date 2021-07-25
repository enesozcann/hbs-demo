using HastaBilgiSistemi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Areas.Doctor.Models
{
    public class AppointmentDetailViewModel
    {
        public AppointmentDto AppointmentDto { get; set; }
        public DiagnosticListDto DiagnosticListDto { get; set; }
        public DiagnosticDto DiagnosticDto { get; set; }
        public RecipeListDto RecipeListDto { get; set; }
    }
}
