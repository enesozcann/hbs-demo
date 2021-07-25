using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Services.Abstract
{
    public interface IPolyclinicService
    {
        Task<IDataResult<PolyclinicDto>> GetAsync(int polyclinicId);
        Task<IDataResult<PolyclinicListDto>> GetAllAsync();
        Task<IDataResult<PolyclinicListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<PolyclinicListDto>> GetAllAppointmentByHospitalAsync(int hospitalId);
        Task<IDataResult<PolyclinicListDto>> GetAllPolyclinicsWithDoctorsByHospitalIdAsync(int hospitalId);
        Task<IResult> AddAsync(PolyclinicAddDto polyclinicAddDto);
        Task<IResult> UpdateAsync(PolyclinicUpdateDto polyclinicUpdateDto);
        Task<IResult> DeleteAsync(int polyclinicId); //isActive = false;
        Task<IResult> HardDeleteAsync(int polyclinicId); // delete from database.
        Task<IDataResult<int>> CountAsync();
    }
}
