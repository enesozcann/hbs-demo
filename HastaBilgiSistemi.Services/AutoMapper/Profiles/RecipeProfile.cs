using AutoMapper;
using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Services.AutoMapper.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<RecipeAddDto, Recipe>().ForMember(destinationMember: dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
        }
    }
}
