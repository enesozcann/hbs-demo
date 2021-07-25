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
    public class MedicineManager : IMedicineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MedicineManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IDataResult<MedicineDto>> AddAsync(MedicineAddDto medicineAddDto)
        {
            var medicine = _mapper.Map<Medicine>(medicineAddDto);
            medicine.IsActive = true;
            var addedMedicine = await _unitOfWork.Medicines.AddAsync(medicine);
            await _unitOfWork.SaveAsync();
            return new DataResult<MedicineDto>(ResultStatus.Success, message: Messages.Medicine.Add(addedMedicine.Name), data: new MedicineDto
            {
                Medicine = addedMedicine,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Medicine.Add(addedMedicine.Name)
            });
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var medicinesCount = await _unitOfWork.Medicines.CountAsync();
            if (medicinesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, medicinesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, Messages.Base.UnExpectedError(), data: -1);
            }
        }

        public async Task<IDataResult<MedicineDto>> DeleteAsync(int medicineId)
        {
            var medicine = await _unitOfWork.Medicines.GetAsync(predicate: a => a.Id == medicineId);
            if (medicine != null)
            {
                medicine.IsDeleted = true;
                var deletedMedicine = await _unitOfWork.Medicines.UpdateAsync(medicine);
                await _unitOfWork.SaveAsync();
                return new DataResult<MedicineDto>(ResultStatus.Success, Messages.Medicine.Delete(deletedMedicine.Name), data: new MedicineDto
                {
                    Medicine = deletedMedicine,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Medicine.Delete(deletedMedicine.Name)
                });
            }
            return new DataResult<MedicineDto>(ResultStatus.Success, message: Messages.Medicine.NotFound(false), data: new MedicineDto
            {
                Medicine = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Medicine.NotFound(false)
            });
        }

        public Task<IDataResult<MedicineListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<MedicineListDto>> GetAllByNonDeletedAsync()
        {
            var medicines = await _unitOfWork.Medicines.GetAllAsync(predicate: a => !a.IsDeleted);
            if (medicines != null)
            {
                return new DataResult<MedicineListDto>(ResultStatus.Success, new MedicineListDto
                {
                    Medicines = medicines,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<MedicineListDto>(ResultStatus.Error, message: Messages.Medicine.NotFound(true), data: new MedicineListDto
            {
                Medicines = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Medicine.NotFound(true)
            });
        }

        public async Task<IDataResult<MedicineDto>> GetAsync(int medicineId)
        {
            var medicine = await _unitOfWork.Medicines.GetAsync(predicate: a => a.Id == medicineId);
            if (medicine != null)
            {
                return new DataResult<MedicineDto>(ResultStatus.Success, new MedicineDto
                {
                    Medicine = medicine,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<MedicineDto>(ResultStatus.Error, message: Messages.Medicine.NotFound(false), data: new MedicineDto
            {
                Medicine = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Medicine.NotFound(false)
            });
        }

        public Task<IDataResult<MedicineUpdateDto>> GetMedicineUpdateDtoAsync(int medicineId)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<MedicineDto>> UpdateAsync(MedicineUpdateDto medicineUpdateDto)
        {
            var oldMedicine = await _unitOfWork.Medicines.GetAsync(a => a.Id == medicineUpdateDto.Id);
            var medicine = _mapper.Map<MedicineUpdateDto, Medicine>(medicineUpdateDto, oldMedicine);
            var updatedMedicine = await _unitOfWork.Medicines.UpdateAsync(medicine);
            await _unitOfWork.SaveAsync();
            return new DataResult<MedicineDto>(ResultStatus.Success, message: Messages.Medicine.Update(updatedMedicine.Name), data: new MedicineDto
            {
                Medicine = updatedMedicine,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Medicine.Update(updatedMedicine.Name)
            });
        }
    }
}
