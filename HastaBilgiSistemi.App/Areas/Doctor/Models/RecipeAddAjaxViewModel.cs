using HastaBilgiSistemi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Areas.Doctor.Models
{
    public class RecipeAddAjaxViewModel
    {
        public RecipeAddDto RecipeAddDto { get; set; }
        public string RecipeAddPartial { get; set; }
        public RecipeDto RecipeDto { get; set; }
    }
}
