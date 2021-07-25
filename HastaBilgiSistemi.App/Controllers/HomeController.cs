using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using HastaBilgiSistemi.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Entities.ComplexTypes;
using HastaBilgiSistemi.App.Models;

namespace HastaBilgiSistemi.App.Controllers
{
    [Authorize(Roles = "Patient")]
    public class HomeController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPolyclinicService _polyclinicService;
        private readonly UserManager<User> _userManager;
        private readonly IPatientService _patientService;

        public HomeController(UserManager<User> userManager, IAppointmentService appointmentService, IPolyclinicService polyclinicService, IPatientService patientService)
        {
            _userManager = userManager;
            _appointmentService = appointmentService;
            _polyclinicService = polyclinicService;
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                var patientDto = await _patientService.GetPatientByUserIdAsync(user.Id);
                if (patientDto != null)
                {
                    var lastAppointment = await _appointmentService.GetAllAppointmentsOnFilter(FilterBy.Patient,false,1,patientDto.Data.Patient.Id,0,DateTime.Now,DateTime.Now);
                    var appointmentListDto = await _appointmentService.GetAllAppointmentsOnFilter(FilterBy.Patient, false, 10, patientDto.Data.Patient.Id, 0, DateTime.Now, DateTime.Now);
                    return View(new PatientHomeViewModel
                    {
                        LastAppointment = lastAppointment.Data,
                        AppointmentListDto = appointmentListDto.Data
                    });
                }
                return NotFound();
            }
            return NotFound();
        }
    }
}
