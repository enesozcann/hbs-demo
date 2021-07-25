using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        [MaxLength(20, ErrorMessage = "{0} {1} karkaterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karkaterden küçük olmamalıdır.")]
        public string UserName { get; set; }
        [DisplayName("Ad")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(1, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string FirstName { get; set; }
        [DisplayName("Soyad")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(1, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string LastName { get; set; }
        [DisplayName("E-Posta")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        [MaxLength(80, ErrorMessage = "{0} {1} karkaterden büyük olmamalıdır.")]
        [MinLength(10, ErrorMessage = "{0} {1} karkaterden küçük olmamalıdır.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        [MaxLength(13, ErrorMessage = "{0} {1} karkaterden büyük olmamalıdır.")]
        [MinLength(13, ErrorMessage = "{0} {1} karkaterden küçük olmamalıdır.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DisplayName("Resim Değiştir")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        [DisplayName("Resim")]
        public string Picture { get; set; }

    }
}
