using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Dtos
{
    public class MedicineAddDto
    {
        [DisplayName("İlaç Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(40, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(3, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("İlaç Prospektüsü")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(540, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(10, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Prospectus { get; set; }
        [DisplayName("ATC Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(40, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(3, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string ATCName { get; set; }
        [DisplayName("Firma Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(40, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(3, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Company { get; set; }
        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public bool IsActive { get; set; }
    }
}
