using AutoMapper;
using HastaBilgiSistemi.Data.Abstract;
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
    public class DoctorManager : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public Task<IDataResult<DoctorDto>> AddAsync(DoctorAddDto doctorAddDto, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<int>> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<DoctorDto>> DeleteAsync(int doctorId)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<DoctorListDto>> GetAllAsync()
        {
            var doctors = await _unitOfWork.Doctors.GetAllAsync();
            if (doctors != null)
            {
                return new DataResult<DoctorListDto>(ResultStatus.Success, new DoctorListDto
                {
                    Doctors = doctors,
                });
            }
            else
            {
                return new DataResult<DoctorListDto>(ResultStatus.Error, Messages.Doctor.NotFound(true), new DoctorListDto
                {
                    Doctors = null,
                });
            }
        }

        public async Task<IDataResult<DoctorListDto>> GetAllByNonDeletedAsync()
        {
            var doctors = await _unitOfWork.Doctors.GetAllAsync(predicate: d=>!d.IsDeleted);
            if (doctors != null)
            {
                return new DataResult<DoctorListDto>(ResultStatus.Success, new DoctorListDto
                {
                    Doctors = doctors,
                });
            }
            else
            {
                return new DataResult<DoctorListDto>(ResultStatus.Error, Messages.Doctor.NotFound(true), new DoctorListDto
                {
                    Doctors = null,
                });
            }
        }

        public async Task<IDataResult<DoctorDto>> GetAsync(int doctorId)
        {
            var doctor = await _unitOfWork.Doctors.GetAsync(predicate: a => a.Id == doctorId, a=>a.User, a=>a.Policlinic.Hospital);
            if (doctor != null)
            {
                return new DataResult<DoctorDto>(ResultStatus.Success, new DoctorDto
                {
                    Doctor = doctor,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<DoctorDto>(ResultStatus.Error, message: Messages.Appointment.NotFound(false), data: new DoctorDto
            {
                Doctor = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Appointment.NotFound(false)
            });
        }

        public async Task<IDataResult<DoctorDto>> GetDoctorByUserIdAsync(int userId)
        {
            var doctor = await _unitOfWork.Doctors.GetAsync(predicate: d => d.UserId == userId);
            if (doctor != null)
            {
                return new DataResult<DoctorDto>(ResultStatus.Success, new DoctorDto
                {
                    Doctor = doctor,
                });
            }
            else
            {
                return new DataResult<DoctorDto>(ResultStatus.Error, Messages.Patient.NotFound(false), new DoctorDto
                {
                    Doctor = null,
                });
            }
        }

        public async Task<IDataResult<DoctorListDto>> GetDoctorsByPolyclinicIdAsync(int polyclinicId)
        {
            var doctors = await _unitOfWork.Doctors.GetAllAsync(predicate: d => d.PoliclinicId == polyclinicId, d=>d.User,d=>d.Policlinic);
            if (doctors != null)
            {
                return new DataResult<DoctorListDto>(ResultStatus.Success, new DoctorListDto
                {
                    Doctors = doctors,
                    ResultStatus = ResultStatus.Success,
                    Message = "Seçtiğiniz poliklinikte bulunan doktorlar listelendi."             
                });
            }
            else
            {
                return new DataResult<DoctorListDto>(ResultStatus.Error, Messages.Doctor.NotFound(true), new DoctorListDto
                {
                    Doctors = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Doctor.NotFound(true)
                });
            }
        }

        public Task<IDataResult<DoctorUpdateDto>> GetDoctorUpdateDtoAsync(int doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> HardDeleteAsync(int doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<DoctorDto>> UpdateAsync(DoctorUpdateDto doctorUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
