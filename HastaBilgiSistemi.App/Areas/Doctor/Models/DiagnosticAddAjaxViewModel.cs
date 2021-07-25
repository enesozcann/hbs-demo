using HastaBilgiSistemi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Areas.Doctor.Models
{
    public class DiagnosticAddAjaxViewModel
    {
        public DiagnosticAddDto DiagnosticAddDto { get; set; }
        public string DiagnosticAddPartial { get; set; }
        public DiagnosticDto DiagnosticDto { get; set; }
    }
}
