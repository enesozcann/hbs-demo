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
    public class PatientUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Kullanıcı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public int UserId { get; set; }
        [DisplayName("Kullanıcı")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        public User User { get; set; }
        public long IdentityNumber { get; set; }
        [DisplayName("Doğum Tarihi")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDay { get; set; }
        [DisplayName("Kilo")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        public int Weight { get; set; }
        [DisplayName("Boy")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        public int Height { get; set; }
        [DisplayName("Adres")]
        [Required(ErrorMessage = "Lütfen bir {0} seçiniz.")]
        public string Address { get; set; }
        [DisplayName("Aktif Mi?")]
        [Required]
        public bool IsActive { get; set; }
    }
}
