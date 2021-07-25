using AutoMapper;
using HastaBilgiSistemi.App.Areas.Admin.Models;
using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Services.Abstract;
using HastaBilgiSistemi.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;
        public AppointmentController(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _appointmentService.GetAllByNonDeletedAsync();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_AppointmentAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(AppointmentAddDto appointmentAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _appointmentService.AddAsync(appointmentAddDto,patientId:13);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var appointmentAddAjaxModel = JsonSerializer.Serialize(new AppointmentAddAjaxViewModel
                    {
                        AppointmentDto = result.Data,
                        AppointmentAddPartial = await this.RenderViewToStringAsync("_AppointmentAddPartial",appointmentAddDto)
                    });
                    return Json(appointmentAddAjaxModel);
                }
            }
            var appointmentAddAjaxErrorModel = JsonSerializer.Serialize(new AppointmentAddAjaxViewModel
            {
                AppointmentAddPartial = await this.RenderViewToStringAsync("_AppointmentAddPartial", appointmentAddDto)
            });
            return Json(appointmentAddAjaxErrorModel);
        }

        public async Task<JsonResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllByNonDeletedAsync();
            var appointmentResult = JsonSerializer.Serialize(appointments, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(appointmentResult);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int appointmentId)
        {
            var result = await _appointmentService.DeleteAsync(appointmentId);
            var deletedAppointment = JsonSerializer.Serialize(result.Data);
            return Json(deletedAppointment);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int appointmentId)
        {
            var result = await _appointmentService.GetAppointmentUpdateDtoAsync(appointmentId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_AppointmentUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(AppointmentUpdateDto appointmentUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _appointmentService.UpdateAsync(appointmentUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var appointmentUpdateAjaxModel = JsonSerializer.Serialize(new AppointmentUpdateAjaxViewModel
                    {
                        AppointmentDto = result.Data,
                        AppointmentUpdatePartial = await this.RenderViewToStringAsync("_AppointmentUpdatePartial", appointmentUpdateDto)
                    });
                    return Json(appointmentUpdateAjaxModel);
                }
            }
            var appointmentUpdateAjaxErrorModel = JsonSerializer.Serialize(new AppointmentUpdateAjaxViewModel
            {
                AppointmentUpdatePartial = await this.RenderViewToStringAsync("_AppointmentUpdatePartial", appointmentUpdateDto)
            });
            return Json(appointmentUpdateAjaxErrorModel);
        }
    }
}
