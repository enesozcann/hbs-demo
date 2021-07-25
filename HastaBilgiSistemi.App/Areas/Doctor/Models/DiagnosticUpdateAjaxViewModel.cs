using HastaBilgiSistemi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Areas.Doctor.Models
{
    public class DiagnosticUpdateAjaxViewModel
    {
        public DiagnosticUpdateDto DiagnosticUpdateDto { get; set; }
        public string DiagnosticUpdatePartial { get; set; }
        public DiagnosticDto DiagnosticDto { get; set; }
    }
}
