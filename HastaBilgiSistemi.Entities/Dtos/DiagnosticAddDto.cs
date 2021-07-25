using HastaBilgiSistemi.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Dtos
{
    public class DiagnosticAddDto
    {
        [DisplayName("Tıbbi Tanı Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(30, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(10, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("Tıbbi Tanı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(140, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(10, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Detail { get; set; }
        [DisplayName("Randevu")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        [DisplayName("Hasta")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
