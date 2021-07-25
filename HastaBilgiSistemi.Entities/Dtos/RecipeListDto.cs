using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Dtos
{
    public class RecipeListDto : DtoGetBase
    {
        public IList<Recipe> Recipes { get; set; }
    }
}
