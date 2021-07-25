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
    public class RecipeAddDto
    {
        [DisplayName("Tıbbi Tanı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public int DiagnosticId { get; set; }
        [DisplayName("Tıbbi Tanı")]
        public Diagnostic Diagnostic { get; set; }
        [DisplayName("İlaç")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public int MedicineId { get; set; }
        [DisplayName("İlaç")]
        public Medicine Medicine { get; set; }
    }
}
