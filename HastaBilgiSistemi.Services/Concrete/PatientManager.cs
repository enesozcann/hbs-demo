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
    public class PatientManager : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public Task<IDataResult<PatientDto>> AddAsync(PatientAddDto patientAddDto, int patientId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<int>> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PatientDto>> DeleteAsync(int patientId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PatientListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PatientListDto>> GetAllByNonDeletedAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PatientDto>> GetAsync(int patientId)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<PatientDto>> GetPatientByUserIdAsync(int userId)
        {
            var patient = await _unitOfWork.Patients.GetAsync(predicate: p => p.UserId == userId);
            if (patient != null)
            {
                return new DataResult<PatientDto>(ResultStatus.Success, new PatientDto
                {
                    Patient = patient,
                });
            }
            else
            {
                return new DataResult<PatientDto>(ResultStatus.Error, Messages.Patient.NotFound(false),new PatientDto
                {
                    Patient = null,
                });
            }      
        }

        public Task<IDataResult<PatientUpdateDto>> GetPatientUpdateDtoAsync(int patientId)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> HardDeleteAsync(int patientId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PatientDto>> UpdateAsync(PatientUpdateDto patientUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
