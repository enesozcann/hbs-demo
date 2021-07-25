using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Services.Abstract
{
    public interface IMedicineService
    {
        Task<IDataResult<MedicineDto>> GetAsync(int medicineId);
        Task<IDataResult<MedicineUpdateDto>> GetMedicineUpdateDtoAsync(int medicineId);
        Task<IDataResult<MedicineListDto>> GetAllAsync();
        Task<IDataResult<MedicineListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<MedicineDto>> AddAsync(MedicineAddDto medicineAddDto);
        Task<IDataResult<MedicineDto>> UpdateAsync(MedicineUpdateDto medicineUpdateDto);
        Task<IDataResult<MedicineDto>> DeleteAsync(int medicineId); //isActive = false;
        Task<IDataResult<int>> CountAsync();
    }
}
