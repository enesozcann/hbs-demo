using AutoMapper;
using HastaBilgiSistemi.App.Models;
using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Services.Abstract;
using HastaBilgiSistemi.Services.Utilities;
using HastaBilgiSistemi.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Controllers
{
    [Authorize(Roles ="Patient")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly UserManager<User> _userManager;
        private readonly IPatientService _patientService;
        private readonly IHospitalService _hospitalService;
        private readonly IPolyclinicService _polyclinicService;
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        public AppointmentController(IAppointmentService appointmentService, IMapper mapper, UserManager<User> userManager, IPatientService patientService, IHospitalService hospitalService, IPolyclinicService polyclinicService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _userManager = userManager;
            _patientService = patientService;
            _hospitalService = hospitalService;
            _polyclinicService = polyclinicService;
            _doctorService = doctorService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(user != null)
            {
                var patient = await _patientService.GetPatientByUserIdAsync(user.Id);
                if(patient.ResultStatus == ResultStatus.Success)
                {
                    var result = await _appointmentService.GetAllAppointmentByPatientAsync(patient.Data.Patient.Id);
                    return View(result.Data);
                }
                return View(new AppointmentListDto
                {
                    Appointments = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Patient.NotFound(false)
                });
            }
            else
            {
                return View(new AppointmentListDto
                {
                    Appointments = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Appointment.NotFound(true)
                });
            }
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int appointmentId)
        {
            var result = await _appointmentService.DeleteAsync(appointmentId);
            var deletedAppointment = JsonSerializer.Serialize(result.Data);
            return Json(deletedAppointment);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var hospitals = await _hospitalService.GetAllByNonDeletedAsync();
            var polyclinics = await _polyclinicService.GetAllByNonDeletedAsync();
            var doctors = await _doctorService.GetAllByNonDeletedAsync();
            return View(new AppointmentAddViewModel { 
                Hospitals = hospitals.Data.Hospitals,
                Polyclinics = polyclinics.Data.Polyclinics,
                Doctors = doctors.Data.Doctors,
            });
            
        }
        [HttpPost]
        public async Task<IActionResult> Add(AppointmentAddViewModel appointmentAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var appointmentAddDto = _mapper.Map<AppointmentAddDto>(appointmentAddViewModel);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var patientResult = await _patientService.GetPatientByUserIdAsync(user.Id);
                var result = await _appointmentService.AddAsync(appointmentAddDto, patientResult.Data.Patient.Id);
                if(result.ResultStatus == ResultStatus.Success)
                {
                    TempData.Add("SuccessMessage",result.Message);
                    return RedirectToAction("Index", "Appointment");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View(appointmentAddViewModel);
                }
            }
            return View(appointmentAddViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> GetPolyclinicList(int hospitalId)
        {
            var result = await _polyclinicService.GetAllAppointmentByHospitalAsync(hospitalId);
            var selectedPolyclinics = JsonSerializer.Serialize(result.Data);
            return Json(selectedPolyclinics);
        }
        [HttpPost]
        public async Task<JsonResult> GetDoctorList(int polyclinicId)
        {
            var result = await _doctorService.GetDoctorsByPolyclinicIdAsync(polyclinicId);
            var selectedPolyclinics = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(selectedPolyclinics);
        }
        [HttpPost]
        public async Task<JsonResult> Cancel(int appointmentId)
        {
            var result = await _appointmentService.DeleteAsync(appointmentId);
            var deletedAppointment = JsonSerializer.Serialize(result.Data);
            return Json(deletedAppointment);
        } 
    }
}
