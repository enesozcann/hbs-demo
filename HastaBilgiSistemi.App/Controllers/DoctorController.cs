using AutoMapper;
using HastaBilgiSistemi.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HastaBilgiSistemi.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HastaBilgiSistemi.App.Controllers
{
    [Authorize(Roles = "Patient")]
    public class DoctorController : Controller
    {
        private readonly IHospitalService _hospitalService;
        private readonly IPolyclinicService _polyclinicService;
        private readonly IDoctorService _doctorService;
        public DoctorController(IHospitalService hospitalService, IPolyclinicService polyclinicService, IDoctorService doctorService)
        {
            _hospitalService = hospitalService;
            _polyclinicService = polyclinicService;
            _doctorService = doctorService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _hospitalService.GetAllByNonDeletedAsync();
            return View(result.Data);
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
            var selectedPolyclinics = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(selectedPolyclinics);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _doctorService.GetAsync(id);
            return View(result.Data);
        }
    }
}
