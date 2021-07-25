using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Dtos
{
    public class UserLoginDto
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        [MaxLength(20, ErrorMessage = "{0} {1} karkaterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karkaterden küçük olmamalıdır.")]
        public string UserName { get; set; }
        [DisplayName("Parola")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karkaterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karkaterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
