using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Entities.Dtos;
using HastaBilgiSistemi.Services.Abstract;
using HastaBilgiSistemi.Shared.Utilities.Results.Abstract;
using HastaBilgiSistemi.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HastaBilgiSistemi.Shared.Utilities.Results.ComplexTypes;
using HastaBilgiSistemi.Shared.Utilities.Results.Concrete;
using AutoMapper;
using System.Linq.Expressions;
using HastaBilgiSistemi.Services.Utilities;
using HastaBilgiSistemi.Entities.ComplexTypes;

namespace HastaBilgiSistemi.Services.Concrete
{
    public class AppointmentManager : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<AppointmentDto>> AddAsync(AppointmentAddDto appointmentAddDto, int patientId)
        {
            var appointment = _mapper.Map<Appointment>(appointmentAddDto);
            appointment.PatientId = patientId;
            appointment.IsActive = true;
            var addedAppointment = await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.SaveAsync();
            return new DataResult<AppointmentDto>(ResultStatus.Success, message: Messages.Appointment.Add(addedAppointment.AppointmentDate),data: new AppointmentDto
            { 
                Appointment = addedAppointment,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Appointment.Add(appointmentAddDto.AppointmentDate)
            });
        }
        public async Task<IDataResult<AppointmentDto>> UpdateAsync(AppointmentUpdateDto appointmentUpdateDto)
        {
            var oldAppointment = await _unitOfWork.Appointments.GetAsync(a => a.Id == appointmentUpdateDto.Id);
            var appointment = _mapper.Map<AppointmentUpdateDto,Appointment>(appointmentUpdateDto, oldAppointment);
            var updatedAppointment = await _unitOfWork.Appointments.UpdateAsync(appointment);
            await _unitOfWork.SaveAsync();
            return new DataResult<AppointmentDto>(ResultStatus.Success, message: Messages.Appointment.Update(updatedAppointment.AppointmentDate), data: new AppointmentDto
            {
                Appointment = updatedAppointment,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Appointment.Update(appointmentUpdateDto.AppointmentDate)
            });
        }
        public async Task<IDataResult<AppointmentDto>> DeleteAsync(int appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetAsync(predicate: a => a.Id == appointmentId);
            if (appointment != null)
            {
                appointment.IsDeleted = true;
                var deletedAppointment = await _unitOfWork.Appointments.UpdateAsync(appointment);
                await _unitOfWork.SaveAsync();
                return new DataResult<AppointmentDto>(ResultStatus.Success, Messages.Appointment.Delete(deletedAppointment.AppointmentDate), data: new AppointmentDto
                {
                    Appointment = deletedAppointment,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Appointment.Delete(deletedAppointment.AppointmentDate)
                });
            }
            return new DataResult<AppointmentDto>(ResultStatus.Success, message: Messages.Appointment.NotFound(false), data: new AppointmentDto
            {
                Appointment = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Appointment.NotFound(false)
            });
        }
        public async Task<IResult> HardDeleteAsync(int appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetAsync(predicate: a => a.Id == appointmentId);
            if (appointment != null)
            {
                await _unitOfWork.Appointments.DeleteAsync(appointment);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, message: Messages.Appointment.HardDelete(appointment.AppointmentDate));
            }
            return new Result(ResultStatus.Error, message: Messages.Appointment.NotFound(false));
        }

        public async Task<IDataResult<AppointmentDto>> GetAsync(int appointmentId)
        {
           var appointment =  await _unitOfWork.Appointments.GetAsync(predicate: a =>a.Id == appointmentId, a=>a.Patient.User);
            if(appointment != null)
            {
                return new DataResult<AppointmentDto>(ResultStatus.Success, new AppointmentDto
                { 
                    Appointment = appointment,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AppointmentDto>(ResultStatus.Error, message: Messages.Appointment.NotFound(false), data:new AppointmentDto
            {
                Appointment = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Appointment.NotFound(false)
            });
        }

        public async Task<IDataResult<AppointmentListDto>> GetAllAsync()
        {
            var appointments = await _unitOfWork.Appointments.GetAllAsync(predicate: null,a=>a.Doctor,a=>a.Patient,a=>a.Doctor.Policlinic,a=>a.Doctor.Policlinic.Hospital,a=>a.Doctor.User,a=>a.Patient.User);
            if(appointments!=null)
            {
                return new DataResult<AppointmentListDto>(ResultStatus.Success, new AppointmentListDto
                { 
                    Appointments = appointments,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AppointmentListDto>(ResultStatus.Error, message: Messages.Appointment.NotFound(true), data: new AppointmentListDto
            { 
                Appointments = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Appointment.NotFound(true)
            });
        }

        public async Task<IDataResult<AppointmentListDto>> GetAllAppointmentByDoctorAsync(int doctorId)
        {
            var appointments = await _unitOfWork.Appointments.GetAllAsync(predicate: a => a.DoctorId == doctorId, includeProperties:a => a.Patient.User);
            if (appointments.Count > -1)
            {
                return new DataResult<AppointmentListDto>(ResultStatus.Success, new AppointmentListDto
                {
                    Appointments = appointments,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AppointmentListDto>(ResultStatus.Error, message: Messages.Appointment.NotFoundWithPredicate("doktora"), data: null);
        }

        public async Task<IDataResult<AppointmentListDto>> GetAllAppointmentByPatientAsync(int patientId)
        {
            var appointments = await _unitOfWork.Appointments.GetAllAsync(predicate: a => a.PatientId == patientId, a=>a.Doctor.User, a=>a.Doctor.Policlinic.Hospital,a=>a.Patient.User);
            if (appointments.Count > -1)
            {
                return new DataResult<AppointmentListDto>(ResultStatus.Success, new AppointmentListDto
                {
                    Appointments = appointments,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AppointmentListDto>(ResultStatus.Error, message: Messages.Appointment.NotFoundWithPredicate("hastaya"), data: null);
        }

        public async Task<IDataResult<AppointmentListDto>> GetAllByNonDeletedAsync()
        {
            var appointments = await _unitOfWork.Appointments.GetAllAsync(predicate: a =>!a.IsDeleted,a=>a.Doctor.Policlinic ,a => a.Doctor.Policlinic.Hospital, a=>a.Patient.User,a=>a.Doctor.User);
            if (appointments != null)
            {
                return new DataResult<AppointmentListDto>(ResultStatus.Success, new AppointmentListDto
                {
                    Appointments = appointments,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AppointmentListDto>(ResultStatus.Error, message: Messages.Appointment.NotFound(true), data: new AppointmentListDto
            {
                Appointments = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Appointment.NotFound(true)
            });
        }

        public async Task<IDataResult<AppointmentUpdateDto>> GetAppointmentUpdateDtoAsync(int appointmentId)
        {
            var result = await _unitOfWork.Appointments.AnyAsync(a => a.Id == appointmentId);
            if (result)
            {
                var appointment = await _unitOfWork.Appointments.GetAsync(a => a.Id == appointmentId, a => a.Doctor, a => a.Patient, a => a.Doctor.Policlinic, a => a.Doctor.Policlinic.Hospital);
                var appointmentUpdateDto = _mapper.Map<AppointmentUpdateDto>(appointment);
                return new DataResult<AppointmentUpdateDto>(ResultStatus.Success, appointmentUpdateDto);
            }
            else
            {
                return new DataResult<AppointmentUpdateDto>(ResultStatus.Error,message: Messages.Appointment.NotFound(false), data: null);
            }
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var appointmentsCount = await _unitOfWork.Appointments.CountAsync();
            if (appointmentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, appointmentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, Messages.Base.UnExpectedError(),data:-1);
            }
        }

        public async Task<IDataResult<AppointmentListDto>> GetAllAppointmentsOnFilter(FilterBy filterBy, bool isAscending, int takeSize, int patientId, int doctorId, DateTime startAt, DateTime endAt)
        {
            IList<Appointment> appointments = new List<Appointment>();
            List<Appointment> sortedAppointments = new List<Appointment>();
            switch (filterBy)
            {
                case FilterBy.Patient:
                    var anyPatient = await _unitOfWork.Patients.AnyAsync(p => p.Id == patientId);
                    if (anyPatient)
                    {
                        appointments = await _unitOfWork.Appointments.GetAllAsync(a => a.PatientId == patientId, a=>a.Doctor.User, a=>a.Doctor.Policlinic.Hospital,a=>a.Patient.User);
                        if (appointments.Count > -1)
                        {
                            sortedAppointments = isAscending ? appointments.Take(takeSize).OrderBy(a => a.AppointmentDate).ToList() : appointments.Take(takeSize).OrderByDescending(a => a.AppointmentDate).ToList();
                        }
                        else { return new DataResult<AppointmentListDto>(ResultStatus.Error, Messages.Appointment.NotFound(true), data: null); }
                    }
                    else { return new DataResult<AppointmentListDto>(ResultStatus.Error, Messages.Patient.NotFound(false), data: null); }
                        
                    break;
                case FilterBy.Doctor:
                    var anyDoctor = await _unitOfWork.Doctors.AnyAsync(d => d.Id == doctorId);
                    if (anyDoctor)
                    {
                        appointments = await _unitOfWork.Appointments.GetAllAsync(a => a.DoctorId == doctorId && a.AppointmentDate >= startAt && a.AppointmentDate <= endAt, a=>a.Patient.User);
                        if (appointments.Count > -1)
                        {
                            sortedAppointments = isAscending ? appointments.Take(takeSize).OrderBy(a => a.AppointmentDate).ToList() : appointments.Take(takeSize).OrderByDescending(a => a.AppointmentDate).ToList();
                        }
                        else { return new DataResult<AppointmentListDto>(ResultStatus.Error, Messages.Appointment.NotFound(true), data: null); }
                    }
                    else { return new DataResult<AppointmentListDto>(ResultStatus.Error, Messages.Doctor.NotFound(false), data: null); }
                    break;
                case FilterBy.AppointmentDate:                   
                    appointments = await _unitOfWork.Appointments.GetAllAsync(a => a.AppointmentDate >= startAt && a.AppointmentDate <= endAt);
                    if (appointments.Count > -1)
                    {
                        sortedAppointments = isAscending ? appointments.Take(takeSize).OrderBy(a => a.AppointmentDate).ToList() : appointments.Take(takeSize).OrderByDescending(a => a.AppointmentDate).ToList();
                    }
                    else { return new DataResult<AppointmentListDto>(ResultStatus.Error, Messages.Appointment.NotFound(true), data: null); }
                    break;
                case FilterBy.Date:
                    appointments = await _unitOfWork.Appointments.GetAllAsync(a => a.ModifiedDate >= startAt && a.ModifiedDate <= endAt);
                    if (appointments.Count > -1)
                    {
                        sortedAppointments = isAscending ? appointments.Take(takeSize).OrderBy(a => a.ModifiedDate).ToList() : appointments.Take(takeSize).OrderByDescending(a => a.ModifiedDate).ToList();
                    }
                    else { return new DataResult<AppointmentListDto>(ResultStatus.Error, Messages.Appointment.NotFound(true), data: null); }
                    break;
                default:
                    break;
            }
            return new DataResult<AppointmentListDto>(ResultStatus.Success, new AppointmentListDto
            {
                Appointments = sortedAppointments,
            });
        }

        public async Task<IDataResult<AppointmentDto>> SetActiveToFalseAsync(int appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetAsync(predicate: a => a.Id == appointmentId);
            if (appointment != null)
            {
                appointment.IsActive = false;
                var inactiveAppointment = await _unitOfWork.Appointments.UpdateAsync(appointment);
                await _unitOfWork.SaveAsync();
                return new DataResult<AppointmentDto>(ResultStatus.Success, Messages.Appointment.Delete(inactiveAppointment.AppointmentDate), data: new AppointmentDto
                {
                    Appointment = inactiveAppointment,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Appointment.Delete(inactiveAppointment.AppointmentDate)
                });
            }
            return new DataResult<AppointmentDto>(ResultStatus.Success, message: Messages.Appointment.NotFound(false), data: new AppointmentDto
            {
                Appointment = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Appointment.NotFound(false)
            });
        }
    }
}
