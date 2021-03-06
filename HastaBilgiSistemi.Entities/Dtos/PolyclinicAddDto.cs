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
    public class PolyclinicAddDto
    {
        [DisplayName("Poliklinik Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(80, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("Hastane")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
