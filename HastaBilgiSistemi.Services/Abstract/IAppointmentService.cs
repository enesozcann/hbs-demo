using HastaBilgiSistemi.Entities.ComplexTypes;
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
    public interface IAppointmentService
    {
        Task<IDataResult<AppointmentDto>> GetAsync(int appointmentId);
        Task<IDataResult<AppointmentUpdateDto>> GetAppointmentUpdateDtoAsync(int appointmentId);
        Task<IDataResult<AppointmentListDto>> GetAllAsync();
        Task<IDataResult<AppointmentListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<AppointmentListDto>> GetAllAppointmentsOnFilter(FilterBy filterBy, bool isAscending, int takeSize, int patientId, int doctorId, DateTime startAt, DateTime endAt);
        Task<IDataResult<AppointmentListDto>> GetAllAppointmentByPatientAsync(int patientId);
        Task<IDataResult<AppointmentListDto>> GetAllAppointmentByDoctorAsync(int doctorId);
        Task<IDataResult<AppointmentDto>> AddAsync(AppointmentAddDto appointmentAddDto, int patientId);
        Task<IDataResult<AppointmentDto>> UpdateAsync(AppointmentUpdateDto appointmentUpdateDto);
        Task<IDataResult<AppointmentDto>> SetActiveToFalseAsync(int appointmentId);
        Task<IDataResult<AppointmentDto>> DeleteAsync(int appointmentId); //isActive = false;
        Task<IResult> HardDeleteAsync(int appointmentId); // delete from database.
        Task<IDataResult<int>> CountAsync();
    }
}
