using AutoMapper;
using HastaBilgiSistemi.Data.Abstract;
using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Services.Abstract;
using HastaBilgiSistemi.Services.Utilities;
using HastaBilgiSistemi.Shared.Utilities.Results.Abstract;
using HastaBilgiSistemi.Shared.Utilities.Results.ComplexTypes;
using HastaBilgiSistemi.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Services.Concrete
{
    public class DiagnosticManager : IDiagnosticService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DiagnosticManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<DiagnosticDto>> AddAsync(DiagnosticAddDto diagnosticAddDto, int patientId, int appointmentId, int doctorId)
        {
            var diagnostic = _mapper.Map<Diagnostic>(diagnosticAddDto);
            diagnostic.PatientId = patientId;
            diagnostic.DoctorId = doctorId;
            diagnostic.AppointmentId = appointmentId;
            diagnostic.IsActive = true;
            var addedDiagnostic = await _unitOfWork.Diagnostics.AddAsync(diagnostic);
            await _unitOfWork.SaveAsync();
            return new DataResult<DiagnosticDto>(ResultStatus.Success, message: Messages.Diagnostic.Add(addedDiagnostic.Name), data: new DiagnosticDto
            {
                Diagnostic = addedDiagnostic,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Diagnostic.Add(addedDiagnostic.Name)
            });
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var diagnosticsCount = await _unitOfWork.Diagnostics.CountAsync();
            if (diagnosticsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, diagnosticsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, Messages.Base.UnExpectedError(), data: -1);
            }
        }

        public async Task<IDataResult<DiagnosticDto>> DeleteAsync(int diagnosticId)
        {
            var diagnostic = await _unitOfWork.Diagnostics.GetAsync(predicate: a => a.Id == diagnosticId);
            if (diagnostic != null)
            {
                diagnostic.IsDeleted = true;
                var deletedDiagnostic = await _unitOfWork.Diagnostics.UpdateAsync(diagnostic);
                await _unitOfWork.SaveAsync();
                return new DataResult<DiagnosticDto>(ResultStatus.Success, Messages.Diagnostic.Delete(deletedDiagnostic.Name), data: new DiagnosticDto
                {
                    Diagnostic = deletedDiagnostic,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Diagnostic.Delete(deletedDiagnostic.Name)
                });
            }
            return new DataResult<DiagnosticDto>(ResultStatus.Success, message: Messages.Diagnostic.NotFound(false), data: new DiagnosticDto
            {
                Diagnostic = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Diagnostic.NotFound(false)
            });
        }

        public Task<IDataResult<DiagnosticListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<DiagnosticListDto>> GetAllByNonDeletedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<DiagnosticListDto>> GetAllDiagnosticsByPatientAsync(int patientId)
        {
            var diagnostics = await _unitOfWork.Diagnostics.GetAllAsync(predicate: a => a.PatientId == patientId && !a.IsDeleted, a => a.Patient.User, a=> a.Doctor.User);
            if (diagnostics.Count > -1)
            {
                return new DataResult<DiagnosticListDto>(ResultStatus.Success, new DiagnosticListDto
                {
                    Diagnostics = diagnostics,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<DiagnosticListDto>(ResultStatus.Error, message: Messages.Diagnostic.NotFoundWithPredicate("hastaya"), data: null);
        }

        public async Task<IDataResult<DiagnosticDto>> GetAsync(int diagnosticId)
        {
            var diagnostic = await _unitOfWork.Diagnostics.GetAsync(predicate: a => a.Id == diagnosticId, a => a.Patient.User, a => a.Doctor.User);
            if (diagnostic != null)
            {
                return new DataResult<DiagnosticDto>(ResultStatus.Success, new DiagnosticDto
                {
                    Diagnostic = diagnostic,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<DiagnosticDto>(ResultStatus.Error, message: Messages.Diagnostic.NotFound(false), data: new DiagnosticDto
            {
                Diagnostic = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Diagnostic.NotFound(false)
            });
        }

        public async Task<IDataResult<DiagnosticUpdateDto>> GetDiagnosticUpdateDtoAsync(int diagnosticId)
        {
            var result = await _unitOfWork.Diagnostics.AnyAsync(a => a.Id == diagnosticId);
            if (result)
            {
                var diagnostic = await _unitOfWork.Diagnostics.GetAsync(a => a.Id == diagnosticId, a => a.Doctor, a => a.Patient, a => a.Appointment);
                var diagnosticUpdateDto = _mapper.Map<DiagnosticUpdateDto>(diagnostic);
                return new DataResult<DiagnosticUpdateDto>(ResultStatus.Success, diagnosticUpdateDto);
            }
            else
            {
                return new DataResult<DiagnosticUpdateDto>(ResultStatus.Error, message: Messages.Diagnostic.NotFound(false), data: null);
            }
        }

        public async Task<IDataResult<DiagnosticDto>> GetLastDiagnosticByAppointmentId(int appointmentId)
        {
            var diagnostics = await _unitOfWork.Diagnostics.GetAllAsync(predicate: a => a.AppointmentId == appointmentId, a => a.Patient.User, a => a.Doctor.User);     
            if (diagnostics != null)
            {
                var lastDiagnostics = diagnostics.OrderBy(d=>d.CreatedDate).LastOrDefault();
                return new DataResult<DiagnosticDto>(ResultStatus.Success, new DiagnosticDto
                {
                    Diagnostic = lastDiagnostics,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<DiagnosticDto>(ResultStatus.Error, message: Messages.Diagnostic.NotFound(false), data: new DiagnosticDto
            {
                Diagnostic = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Diagnostic.NotFound(false)
            });
        }

        public async Task<IDataResult<DiagnosticDto>> UpdateAsync(DiagnosticUpdateDto diagnosticUpdateDto)
        {
            var oldDiagnostic = await _unitOfWork.Diagnostics.GetAsync(a => a.Id == diagnosticUpdateDto.Id);
            var diagnostic = _mapper.Map<DiagnosticUpdateDto, Diagnostic>(diagnosticUpdateDto, oldDiagnostic);
            var updatedDiagnostic = await _unitOfWork.Diagnostics.UpdateAsync(diagnostic);
            await _unitOfWork.SaveAsync();
            return new DataResult<DiagnosticDto>(ResultStatus.Success, message: Messages.Diagnostic.Update(updatedDiagnostic.Name), data: new DiagnosticDto
            {
                Diagnostic = updatedDiagnostic,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Diagnostic.Update(diagnosticUpdateDto.Name)
            });
        }
    }
}
