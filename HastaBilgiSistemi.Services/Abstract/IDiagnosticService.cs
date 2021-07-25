using HastaBilgiSistemi.Entities.ComplexTypes;
using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Services.Abstract
{
    public interface IDiagnosticService
    {
        Task<IDataResult<DiagnosticDto>> GetAsync(int diagnosticId);
        Task<IDataResult<DiagnosticDto>> GetLastDiagnosticByAppointmentId(int appointmentId);
        Task<IDataResult<DiagnosticUpdateDto>> GetDiagnosticUpdateDtoAsync(int diagnosticId);
        Task<IDataResult<DiagnosticListDto>> GetAllAsync();
        Task<IDataResult<DiagnosticListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<DiagnosticListDto>> GetAllDiagnosticsByPatientAsync(int patientId);
        Task<IDataResult<DiagnosticDto>> AddAsync(DiagnosticAddDto diagnosticAddDto, int patientId, int appointmentId, int doctorId);
        Task<IDataResult<DiagnosticDto>> UpdateAsync(DiagnosticUpdateDto diagnosticUpdateDto);
        Task<IDataResult<DiagnosticDto>> DeleteAsync(int diagnosticId); //isActive = false;
        Task<IDataResult<int>> CountAsync();
    }
}
