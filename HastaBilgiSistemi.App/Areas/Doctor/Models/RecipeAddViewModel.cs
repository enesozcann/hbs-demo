using HastaBilgiSistemi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Areas.Doctor.Models
{
    public class RecipeAddViewModel
    {
        public MedicineListDto MedicineListDto { get; set; }
        public RecipeAddDto RecipeAddDto { get; set; }
    }
}
