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
    public class AppointmentAddDto
    {
        [DisplayName("Randevu Tarihi")]
        [Required(ErrorMessage ="Lütfen bir {0} seçiniz.")]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime AppointmentDate { get; set; }
        [DisplayName("Doktor")]
        [Required(ErrorMessage ="{0} alanı boş geçilmemelidir.")]
        public int DoctorId { get; set; }
        [DisplayName("Doktor")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        public Doctor Doctor { get; set; }
        [DisplayName("Aktif Mi?")]
        [Required]
        public bool IsActive { get; set; }
    }
}
