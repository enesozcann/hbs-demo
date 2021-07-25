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
    public class HospitalProfile:Profile
    {
        public HospitalProfile()
        {
            CreateMap<HospitalAddDto, Hospital>().ForMember(destinationMember: dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<HospitalUpdateDto, Hospital>().ForMember(destinationMember: dest=>dest.ModifiedDate, opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<Hospital, HospitalUpdateDto>();
        }
    }
}
