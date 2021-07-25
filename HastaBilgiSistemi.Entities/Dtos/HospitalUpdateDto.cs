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
    public class HospitalUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Hastane Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(120, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(360, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Address { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
