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
    public interface IHospitalService
    {
        Task<IDataResult<HospitalDto>> GetAsync(int hospitalId);
        Task<IDataResult<HospitalUpdateDto>> GetHospitalUpdateDtoAsync(int hospitalId);
        Task<IDataResult<HospitalListDto>> GetAllAsync();
        Task<IDataResult<HospitalListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<HospitalDto>> AddAsync(HospitalAddDto hospitalAddDto);
        Task<IDataResult<HospitalDto>> UpdateAsync(HospitalUpdateDto hospitalUpdateDto);
        Task<IDataResult<HospitalDto>> DeleteAsync(int hospitalId); //isActive = false;
        Task<IResult> HardDeleteAsync(int hospitalId); // delete from database.
        Task<IDataResult<int>> CountAsync();
    }
}
