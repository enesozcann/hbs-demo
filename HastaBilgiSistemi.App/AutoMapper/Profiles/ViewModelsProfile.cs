using AutoMapper;
using HastaBilgiSistemi.App.Models;
using HastaBilgiSistemi.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.AutoMapper.Profiles
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<AppointmentAddViewModel, AppointmentAddDto>();
        }
    }
}
