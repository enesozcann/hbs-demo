using AutoMapper;
using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HastaBilgiSistemi.Shared.Utilities.Results.ComplexTypes;
using HastaBilgiSistemi.App.Areas.Admin.Models;

namespace HastaBilgiSistemi.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPolyclinicService _polyclinicService;
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager, IAppointmentService appointmentService, IPolyclinicService polyclinicService)
        {
            _userManager = userManager;
            _appointmentService = appointmentService;
            _polyclinicService = polyclinicService;
        }

        public async Task<IActionResult> Index()
        {
            var appointmentsCountResult = await _appointmentService.CountAsync();
            var polyclinicCountResult = await _polyclinicService.CountAsync();
            var usersCount = await _userManager.Users.CountAsync();

            if(appointmentsCountResult.ResultStatus == ResultStatus.Success && polyclinicCountResult.ResultStatus == ResultStatus.Success && usersCount > -1)
            {
                return View(new DashboardViewModel { 
                    PolyclinicsCount = polyclinicCountResult.Data,
                    AppointmentsCount = appointmentsCountResult.Data,
                    UsersCount = usersCount,       
                });
            }return NotFound();
        }
    }
}
