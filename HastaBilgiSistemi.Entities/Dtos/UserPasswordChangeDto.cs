using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Dtos
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Mevcut Parola")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karkaterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karkaterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [DisplayName("Yeni Parola")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karkaterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karkaterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DisplayName("Yeni Parola Tekrar")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karkaterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karkaterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Girmiş olduğunuz parolalar eşleşmiyor.")]
        public string RepeatPassword { get; set; }

    }
}
