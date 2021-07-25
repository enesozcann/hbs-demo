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
    public class HospitalController : Controller
    {
        private readonly IHospitalService _hospitalService;
        private readonly IMapper _mapper;
        public HospitalController(IHospitalService hospitalService, IMapper mapper)
        {
            _hospitalService = hospitalService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _hospitalService.GetAllByNonDeletedAsync();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_HospitalAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(HospitalAddDto hospitalAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _hospitalService.AddAsync(hospitalAddDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var hospitalAddAjaxModel = JsonSerializer.Serialize(new HospitalAddAjaxViewModel
                    {
                        HospitalDto = result.Data,
                        HospitalAddPartial = await this.RenderViewToStringAsync("_HospitalAddPartial",hospitalAddDto)
                    });
                    return Json(hospitalAddAjaxModel);
                }
            }
            var hospitalAddAjaxErrorModel = JsonSerializer.Serialize(new HospitalAddAjaxViewModel
            {
                HospitalAddPartial = await this.RenderViewToStringAsync("_HospitalAddPartial", hospitalAddDto)
            });
            return Json(hospitalAddAjaxErrorModel);
        }

        public async Task<JsonResult> GetAllHospitals()
        {
            var hospitals = await _hospitalService.GetAllByNonDeletedAsync();
            var hospitalListDto = JsonSerializer.Serialize(hospitals, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(hospitalListDto);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int hospitalId)
        {
            var result = await _hospitalService.DeleteAsync(hospitalId);
            var deletedHospital = JsonSerializer.Serialize(result.Data);
            return Json(deletedHospital);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int hospitalId)
        {
            var result = await _hospitalService.GetHospitalUpdateDtoAsync(hospitalId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_HospitalUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(HospitalUpdateDto hospitalUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _hospitalService.UpdateAsync(hospitalUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var hospitalUpdateAjaxModel = JsonSerializer.Serialize(new HospitalUpdateAjaxViewModel
                    {
                        HospitalDto = result.Data,
                        HospitalUpdatePartial = await this.RenderViewToStringAsync("_HospitalUpdatePartial", hospitalUpdateDto)
                    });
                    return Json(hospitalUpdateAjaxModel);
                }
            }
            var hospitalUpdateAjaxErrorModel = JsonSerializer.Serialize(new HospitalUpdateAjaxViewModel
            {
                HospitalUpdatePartial = await this.RenderViewToStringAsync("_HospitalUpdatePartial", hospitalUpdateDto)
            });
            return Json(hospitalUpdateAjaxErrorModel);
        }
    }
}
