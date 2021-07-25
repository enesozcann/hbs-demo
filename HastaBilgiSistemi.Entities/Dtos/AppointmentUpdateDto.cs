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
    public class AppointmentUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Randevu Tarihi")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        public DateTime AppointmentDate { get; set; }
        [DisplayName("Doktor")]
        [Required]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        [DisplayName("Randevu Durumu")]
        [Required]
        public bool IsActive { get; set; }
        [DisplayName("Silinsin mi?")]
        [Required]
        public bool IsDeleted { get; set; }
    }
}
