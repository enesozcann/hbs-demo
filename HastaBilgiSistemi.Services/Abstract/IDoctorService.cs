using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Services.Abstract
{
    public interface IDoctorService
    {
        Task<IDataResult<DoctorDto>> GetAsync(int doctorId);
        Task<IDataResult<DoctorDto>> GetDoctorByUserIdAsync(int userId);
        Task<IDataResult<DoctorUpdateDto>> GetDoctorUpdateDtoAsync(int doctorId);
        Task<IDataResult<DoctorListDto>> GetDoctorsByPolyclinicIdAsync(int polyclinicId);
        Task<IDataResult<DoctorListDto>> GetAllAsync();
        Task<IDataResult<DoctorListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<DoctorDto>> AddAsync(DoctorAddDto doctorAddDto, int userId);
        Task<IDataResult<DoctorDto>> UpdateAsync(DoctorUpdateDto doctorUpdateDto);
        Task<IDataResult<DoctorDto>> DeleteAsync(int doctorId); //isActive = false;
        Task<IResult> HardDeleteAsync(int doctorId); // delete from database.
        Task<IDataResult<int>> CountAsync();
    }
}
