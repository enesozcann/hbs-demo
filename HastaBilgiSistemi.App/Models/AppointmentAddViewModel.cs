using HastaBilgiSistemi.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Models
{
    public class AppointmentAddViewModel
    {
        [DisplayName("Randevu Tarihi")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime AppointmentDate { get; set; }
        [DisplayName("Doktor")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
        public int PolyclinicId { get; set; }

        public IList<Hospital> Hospitals { get; set; }
        public IList<Polyclinic> Polyclinics { get; set; }
        public IList<Doctor> Doctors { get; set; }
    }
}
