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
    public class HospitalManager : IHospitalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HospitalManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<HospitalDto>> AddAsync(HospitalAddDto hospitalAddDto)
        {
            var hospital = _mapper.Map<Hospital>(hospitalAddDto);
            var addedHospital = await _unitOfWork.Hospitals.AddAsync(hospital);
            await _unitOfWork.SaveAsync();
            return new DataResult<HospitalDto>(ResultStatus.Success, message: Messages.Hospital.Add(addedHospital.Name), data: new HospitalDto
            {
                Hospital = addedHospital,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Hospital.Add(addedHospital.Name)
            });
        }
        public async Task<IDataResult<HospitalDto>> UpdateAsync(HospitalUpdateDto hospitalUpdateDto)
        {
            var hospital = _mapper.Map<Hospital>(hospitalUpdateDto);
            var updatedHospital = await _unitOfWork.Hospitals.UpdateAsync(hospital);
            await _unitOfWork.SaveAsync();
            return new DataResult<HospitalDto>(ResultStatus.Success, message: Messages.Hospital.Update(updatedHospital.Name), data: new HospitalDto
            {
                Hospital = updatedHospital,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Hospital.Update(updatedHospital.Name)
            });
        }

        public async Task<IDataResult<HospitalDto>> DeleteAsync(int hospitalId)
        {
            var hospital = await _unitOfWork.Hospitals.GetAsync(predicate: h => h.Id == hospitalId);
            if (hospital != null)
            {
                hospital.IsDeleted = true;
                var deletedHospital = await _unitOfWork.Hospitals.UpdateAsync(hospital);
                await _unitOfWork.SaveAsync();
                return new DataResult<HospitalDto>(ResultStatus.Success, Messages.Hospital.Delete(deletedHospital.Name), data: new HospitalDto
                {
                    Hospital = deletedHospital,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Hospital.Delete(deletedHospital.Name)
                });
            }
            return new DataResult<HospitalDto>(ResultStatus.Success, message: Messages.Hospital.NotFound(false), data: new HospitalDto
            {
                Hospital = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Hospital.NotFound(false)
            });
        }

        public async Task<IResult> HardDeleteAsync(int hospitalId)
        {
            var result = await _unitOfWork.Hospitals.AnyAsync(h => h.Id == hospitalId);
            if (result)
            {
                var hospital = await _unitOfWork.Hospitals.GetAsync(h => h.Id == hospitalId);
                await _unitOfWork.Hospitals.DeleteAsync(hospital);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, message: Messages.Hospital.HardDelete(hospital.Name));
            }
            return new Result(ResultStatus.Error, message: Messages.Hospital.NotFound(false));
        }

        public async Task<IDataResult<HospitalDto>> GetAsync(int hospitalId)
        {
            var hospital = await _unitOfWork.Hospitals.GetAsync(predicate: h => h.Id == hospitalId);
            if (hospital !=null)
            {
                return new DataResult<HospitalDto>(ResultStatus.Success, data: new HospitalDto
                {
                    Hospital = hospital,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<HospitalDto>(ResultStatus.Error, message: Messages.Hospital.NotFound(false), data: null);
        }

        public async Task<IDataResult<HospitalListDto>> GetAllAsync()
        {
            var hospital = await _unitOfWork.Hospitals.GetAllAsync(predicate: null);
            if (hospital.Count > -1)
            {
                return new DataResult<HospitalListDto>(ResultStatus.Success, data: new HospitalListDto
                {
                    Hospitals = hospital,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<HospitalListDto>(ResultStatus.Error,message: Messages.Hospital.NotFound(true), data: null);
        }

        public async Task<IDataResult<HospitalListDto>> GetAllByNonDeletedAsync()
        {
            var hospital = await _unitOfWork.Hospitals.GetAllAsync(predicate: h => !h.IsDeleted);
            if (hospital.Count > -1)
            {
                return new DataResult<HospitalListDto>(ResultStatus.Success, data: new HospitalListDto
                {
                    Hospitals = hospital,
                    ResultStatus = ResultStatus.Success
                            
                });
            }
            return new DataResult<HospitalListDto>(ResultStatus.Error, message: Messages.Hospital.NotFound(true), data: null);
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var hospitalCount = await _unitOfWork.Hospitals.CountAsync();
            if (hospitalCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, hospitalCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, Messages.Base.UnExpectedError(), data: -1);
            }
        }

        public async Task<IDataResult<HospitalUpdateDto>> GetHospitalUpdateDtoAsync(int hospitalId)
        {
            var result = await _unitOfWork.Hospitals.AnyAsync(h => h.Id == hospitalId);
            if (result)
            {
                var hospital = await _unitOfWork.Hospitals.GetAsync(h => h.Id == hospitalId);
                var hospitalUpdateDto = _mapper.Map<HospitalUpdateDto>(hospital);
                return new DataResult<HospitalUpdateDto>(ResultStatus.Success, hospitalUpdateDto);
            }
            else
            {
                return new DataResult<HospitalUpdateDto>(ResultStatus.Error, message: Messages.Hospital.NotFound(false), data: null);
            }
        }
    }
}
