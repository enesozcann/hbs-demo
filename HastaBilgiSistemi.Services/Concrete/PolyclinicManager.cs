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
    public class PolyclinicManager : IPolyclinicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PolyclinicManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> AddAsync(PolyclinicAddDto polyclinicAddDto)
        {
            var polyclinic = _mapper.Map<Polyclinic>(polyclinicAddDto);
            await _unitOfWork.Polyclinics.AddAsync(polyclinic);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, message: Messages.Polyclinic.Add(polyclinic.Name));
        }
        public async Task<IResult> UpdateAsync(PolyclinicUpdateDto polyclinicUpdateDto)
        {
            var polyclinic = _mapper.Map<Polyclinic>(polyclinicUpdateDto);
            await _unitOfWork.Polyclinics.UpdateAsync(polyclinic);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, message: Messages.Polyclinic.Update(polyclinic.Name));
        }

        public async Task<IResult> DeleteAsync(int polyclinicId)
        {
            var result = await _unitOfWork.Polyclinics.AnyAsync(p => p.Id == polyclinicId);
            if (result)
            {
                var polyclinic = await _unitOfWork.Polyclinics.GetAsync(p => p.Id == polyclinicId);
                polyclinic.IsDeleted = true;
                polyclinic.ModifiedDate = DateTime.Now;
                await _unitOfWork.Polyclinics.UpdateAsync(polyclinic);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, message: Messages.Polyclinic.Delete(polyclinic.Name));
            }
            return new Result(ResultStatus.Error, message: Messages.Polyclinic.NotFound(false));
        }

        public async Task<IResult> HardDeleteAsync(int polyclinicId)
        {
            var result = await _unitOfWork.Polyclinics.AnyAsync(p => p.Id == polyclinicId);
            if (result)
            {
                var polyclinic = await _unitOfWork.Polyclinics.GetAsync(p => p.Id == polyclinicId);
                await _unitOfWork.Polyclinics.DeleteAsync(polyclinic);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, message: Messages.Polyclinic.HardDelete(polyclinic.Name));
            }
            return new Result(ResultStatus.Error, message: Messages.Polyclinic.NotFound(false));
        }

        public async Task<IDataResult<PolyclinicDto>> GetAsync(int polyclinicId)
        {
            var polyclinic = await _unitOfWork.Polyclinics.GetAsync(predicate: p => p.Id == polyclinicId, includeProperties: p=>p.Hospital);
            if (polyclinic !=null)
            {
                return new DataResult<PolyclinicDto>(ResultStatus.Success, data: new PolyclinicDto
                {
                    Polyclinic = polyclinic,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PolyclinicDto>(ResultStatus.Error, message: Messages.Polyclinic.NotFound(false), data: null);
        }

        public async Task<IDataResult<PolyclinicListDto>> GetAllAsync()
        {
            var polyclinic = await _unitOfWork.Polyclinics.GetAllAsync(predicate: null, includeProperties: p => p.Hospital);
            if (polyclinic.Count > -1)
            {
                return new DataResult<PolyclinicListDto>(ResultStatus.Success, data: new PolyclinicListDto
                {
                    Polyclinics = polyclinic,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PolyclinicListDto>(ResultStatus.Error,message: Messages.Polyclinic.NotFound(true), data: null);
        }

        public async Task<IDataResult<PolyclinicListDto>> GetAllAppointmentByHospitalAsync(int hospitalId)
        {
            var result = await _unitOfWork.Hospitals.AnyAsync(h => h.Id == hospitalId);
            if (result)
            {
                var polyclinic = await _unitOfWork.Polyclinics.GetAllAsync(predicate: p => p.HospitalId == hospitalId && !p.IsDeleted);
                if (polyclinic.Count > -1)
                {
                    return new DataResult<PolyclinicListDto>(ResultStatus.Success, data: new PolyclinicListDto
                    {
                        Polyclinics = polyclinic,
                        ResultStatus = ResultStatus.Success,
                        Message = "Seçtiğiniz hastane içerisinde bulunan poliklinikler listelendi."
                    });
                }
                return new DataResult<PolyclinicListDto>(ResultStatus.Error, message: Messages.Polyclinic.NotFound(true), data: null);
            }
            return new DataResult<PolyclinicListDto>(ResultStatus.Error, message: Messages.Polyclinic.NotFoundWithPredicate("hastaneye"), data: null);

        }

        public async Task<IDataResult<PolyclinicListDto>> GetAllByNonDeletedAsync()
        {
            var polyclinic = await _unitOfWork.Polyclinics.GetAllAsync(predicate: p => !p.IsDeleted, includeProperties: p=>p.Hospital);
            if (polyclinic.Count > -1)
            {
                return new DataResult<PolyclinicListDto>(ResultStatus.Success, data: new PolyclinicListDto
                {
                    Polyclinics = polyclinic,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PolyclinicListDto>(ResultStatus.Error, message: Messages.Polyclinic.NotFound(true), data: null);
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var polyclinicCount = await _unitOfWork.Polyclinics.CountAsync();
            if (polyclinicCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, polyclinicCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, Messages.Base.UnExpectedError(), data: -1);
            }
        }

        public async Task<IDataResult<PolyclinicListDto>> GetAllPolyclinicsWithDoctorsByHospitalIdAsync(int hospitalId)
        {
            var polyclinic = await _unitOfWork.Polyclinics.GetAllAsync(predicate: p => !p.IsDeleted && p.HospitalId == hospitalId);
            if (polyclinic !=null)
            {
                return new DataResult<PolyclinicListDto>(ResultStatus.Success, data: new PolyclinicListDto
                {
                    Polyclinics = polyclinic,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PolyclinicListDto>(ResultStatus.Error, message: Messages.Polyclinic.NotFound(true), data: null);
        }
    }
}
