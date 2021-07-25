using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HastaBilgiSistemi.Entities.ComplexTypes;
using HastaBilgiSistemi.Shared.Utilities.Results.ComplexTypes;
using AutoMapper;
using HastaBilgiSistemi.App.Areas.Doctor.Models;
using HastaBilgiSistemi.Entities.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HastaBilgiSistemi.App.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Authorize(Roles = "Doctor")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly UserManager<User> _userManager;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IDiagnosticService _diagnosticService;
        private readonly IRecipeService _recipeService;
        private readonly IMedicineService _medicineService;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentService appointmentService, UserManager<User> userManager, IPatientService patientService, IDoctorService doctorService, IMapper mapper, IDiagnosticService diagnosticService, IRecipeService recipeService, IMedicineService medicineService)
        {
            _appointmentService = appointmentService;
            _userManager = userManager;
            _patientService = patientService;
            _doctorService = doctorService;
            _diagnosticService = diagnosticService;
            _recipeService = recipeService;
            _medicineService = medicineService;
            
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var doctor = await _doctorService.GetDoctorByUserIdAsync(user.Id);
            var result = await _appointmentService.GetAllAppointmentByDoctorAsync(doctor.Data.Doctor.Id);
            return View(result.Data);
        }
        public async Task<IActionResult> Today()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var doctor = await _doctorService.GetDoctorByUserIdAsync(user.Id);
            DateTime date = DateTime.Today;
            var result = await _appointmentService.GetAllAppointmentsOnFilter(FilterBy.Doctor, false, 100, 0, doctor.Data.Doctor.Id, date, date.AddDays(1));
            return View(result.Data);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var appointmentDto = await _appointmentService.GetAsync(id);
            var diagnosticListDto = await _diagnosticService.GetAllDiagnosticsByPatientAsync(appointmentDto.Data.Appointment.Patient.Id);
            var diagnostic = await _diagnosticService.GetLastDiagnosticByAppointmentId(appointmentDto.Data.Appointment.Id);
            if (appointmentDto.ResultStatus == ResultStatus.Success)
            {
                    if(diagnostic.Data.Diagnostic != null)
                    {
                        var recipeListDto = await _recipeService.GetAllRecipesByDiagnosticAsync(diagnostic.Data.Diagnostic.Id);
                        return View(new AppointmentDetailViewModel
                        {
                            AppointmentDto = appointmentDto.Data,
                            DiagnosticListDto = diagnosticListDto.Data,
                            DiagnosticDto = diagnostic.Data,
                            RecipeListDto = recipeListDto.Data
                        });
                    }
                    else
                    {
                        return View(new AppointmentDetailViewModel
                        {
                            AppointmentDto = appointmentDto.Data,
                            DiagnosticListDto = diagnosticListDto.Data,
                            DiagnosticDto = diagnostic.Data,
                            RecipeListDto = null,
                        });
                    }
            
            }
            else { return NotFound(); }
        }
        [HttpGet("Doctor/Appointment/DiagnosticAdd")]
        public IActionResult DiagnosticAdd(int patientId, int appointmentId)
        {
            var DiagnosticAddDto = new DiagnosticAddDto
            {
                PatientId = patientId,
                AppointmentId = appointmentId
            };
            return PartialView("_DiagnosticAddPartial", DiagnosticAddDto);
        }
        [HttpPost]
        public async Task<IActionResult> DiagnosticAdd(DiagnosticAddDto diagnosticAddDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var doctor = await _doctorService.GetDoctorByUserIdAsync(user.Id);
                //var diagnostic = _mapper.Map<Diagnostic>(diagnosticAddDto);
                var result = await _diagnosticService.AddAsync(diagnosticAddDto, diagnosticAddDto.PatientId,diagnosticAddDto.AppointmentId,doctor.Data.Doctor.Id);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new DiagnosticAddAjaxViewModel
                    {
                        DiagnosticDto = new DiagnosticDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"Tanı başarıyla oluşturuldu.",
                            Diagnostic = result.Data.Diagnostic
                        },
                        DiagnosticAddPartial = await this.RenderViewToStringAsync("_DiagnosticAddPartial", diagnosticAddDto)
                    });
                    return Json(userAddAjaxModel);
                }
                else
                {
                    foreach (var error in result.Exception.Data)
                    {
                        ModelState.AddModelError("", error.ToString());
                    }
                    var diagnosticAddAjaxErrorModel = JsonSerializer.Serialize(new DiagnosticAddAjaxViewModel
                    {
                        DiagnosticAddDto = diagnosticAddDto,
                        DiagnosticAddPartial = await this.RenderViewToStringAsync("_DiagnosticAddPartial", diagnosticAddDto)

                    });
                    return Json(diagnosticAddAjaxErrorModel);
                }
            }
            var diagnosticAddAjaxModelStateErrorModel = JsonSerializer.Serialize(new DiagnosticAddAjaxViewModel
            {
                DiagnosticAddDto = diagnosticAddDto,
                DiagnosticAddPartial = await this.RenderViewToStringAsync("_DiagnosticAddPartial", diagnosticAddDto)

            });
            return Json(diagnosticAddAjaxModelStateErrorModel);
        }

        [HttpGet]
        public async Task<IActionResult> DiagnosticUpdate(int id)
        {
            var diagnosticUpdateDto = await _diagnosticService.GetDiagnosticUpdateDtoAsync(id);
            return PartialView("_DiagnosticUpdatePartial", diagnosticUpdateDto.Data);
        }
        [HttpPost]
        public async Task<IActionResult> DiagnosticUpdate(DiagnosticUpdateDto diagnosticUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _diagnosticService.UpdateAsync(diagnosticUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var diagnosticUpdateAjaxModel = JsonSerializer.Serialize(new DiagnosticUpdateAjaxViewModel
                    {
                        DiagnosticDto = result.Data,
                        DiagnosticUpdatePartial = await this.RenderViewToStringAsync("_DiagnosticUpdatePartial", diagnosticUpdateDto)
                    });
                    return Json(diagnosticUpdateAjaxModel);
                }
            }
            var appointmentUpdateAjaxErrorModel = JsonSerializer.Serialize(new DiagnosticUpdateAjaxViewModel
            {
                DiagnosticUpdatePartial = await this.RenderViewToStringAsync("_DiagnosticUpdatePartial", diagnosticUpdateDto)
            });
            return Json(appointmentUpdateAjaxErrorModel);
        }

        [HttpPost]
        public async Task<JsonResult> SetIsActiveToFalse(int id)
        {
            var appointmentUpdateDto = await _appointmentService.SetActiveToFalseAsync(id);
            if(appointmentUpdateDto.ResultStatus == ResultStatus.Success)
            {
               return Json(new { success = appointmentUpdateDto.ResultStatus, responseText = appointmentUpdateDto.Message });
            }
            else
            {
                return Json(new { error = appointmentUpdateDto.ResultStatus, responseText = appointmentUpdateDto.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> RecipeAdd(int id)
        {
            var medicineListDto = await _medicineService.GetAllByNonDeletedAsync();
            var RecipeAddViewModel = new RecipeAddViewModel
            {
                RecipeAddDto = new RecipeAddDto
                {
                    DiagnosticId = id
                },
                MedicineListDto = medicineListDto.Data
            };
            return PartialView("_RecipeAddPartial", RecipeAddViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> RecipeAdd(RecipeAddDto recipeAddDto)
        {
            if (ModelState.IsValid)
            {
                var medicineListDto = await _medicineService.GetAllByNonDeletedAsync();
                var recipe = _mapper.Map<Recipe>(recipeAddDto);
                var medicine = await _medicineService.GetAsync(recipe.MedicineId);
                recipe.Medicine = medicine.Data.Medicine;
                var result = await _recipeService.AddAsync(recipeAddDto, recipeAddDto.DiagnosticId, recipeAddDto.MedicineId);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var recipeAddAjaxModel = JsonSerializer.Serialize(new RecipeAddAjaxViewModel
                    {
                        RecipeDto = new RecipeDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"İlaç eklendi.",
                            Recipe = recipe
                        },
                        RecipeAddPartial = await this.RenderViewToStringAsync("_RecipeAddPartial", new RecipeAddViewModel
                        {
                            RecipeAddDto = recipeAddDto,
                            MedicineListDto = medicineListDto.Data
                        })
                    });
                    return Json(recipeAddAjaxModel);
                }
                else
                {
                    foreach (var error in result.Exception.Data)
                    {
                        ModelState.AddModelError("", error.ToString());
                    }
                    var recipeAddAjaxErrorModel = JsonSerializer.Serialize(new RecipeAddAjaxViewModel
                    {
                        RecipeAddDto = recipeAddDto,
                        RecipeAddPartial = await this.RenderViewToStringAsync("_RecipeAddPartial", recipeAddDto)

                    });
                    return Json(recipeAddAjaxErrorModel);
                }
            }
            var recipeAddAjaxModelStateErrorModel = JsonSerializer.Serialize(new RecipeAddAjaxViewModel
            {
                RecipeAddDto = recipeAddDto,
                RecipeAddPartial = await this.RenderViewToStringAsync("_RecipeAddPartial", recipeAddDto)

            });
            return Json(recipeAddAjaxModelStateErrorModel);
        }

        [HttpPost]
        public async Task<JsonResult> RecipeDelete(int recipeId)
        {
            var recipe = await _recipeService.GetAsync(recipeId);
            var result = await _recipeService.DeleteAsync(recipeId);
            if (result.ResultStatus==ResultStatus.Success)
            {
                var deletedRecipe = JsonSerializer.Serialize(new RecipeDto
                {
                    ResultStatus = ResultStatus.Success,
                    Message = "İlaç silindi.",
                    Recipe = recipe.Data.Recipe
                }, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                });
                return Json(deletedRecipe);
            }
            else
            {
                var deletedRecipeErrorModel = JsonSerializer.Serialize(new RecipeDto
                {
                    ResultStatus = ResultStatus.Error,
                    Message = $"İlaç silinirken hata oluştu.\n{result.Message}",
                    Recipe = recipe.Data.Recipe
                });
                return Json(deletedRecipeErrorModel);
            }
        }
    }
}
