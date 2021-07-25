using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Services.Abstract
{
    public interface IPatientService
    {
        Task<IDataResult<PatientDto>> GetAsync(int patientId);
        Task<IDataResult<PatientDto>> GetPatientByUserIdAsync(int userId);
        Task<IDataResult<PatientUpdateDto>> GetPatientUpdateDtoAsync(int patientId);
        Task<IDataResult<PatientListDto>> GetAllAsync();
        Task<IDataResult<PatientListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<PatientDto>> AddAsync(PatientAddDto patientAddDto, int patientId);
        Task<IDataResult<PatientDto>> UpdateAsync(PatientUpdateDto patientUpdateDto);
        Task<IDataResult<PatientDto>> DeleteAsync(int patientId); //isActive = false;
        Task<IResult> HardDeleteAsync(int patientId); // delete from database.
        Task<IDataResult<int>> CountAsync();
    }
}
