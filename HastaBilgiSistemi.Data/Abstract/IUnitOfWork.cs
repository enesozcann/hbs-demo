using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Data.Abstract
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        IAppointmentRepository Appointments { get; } // unitofwork.Appointments
        IDoctorRepository Doctors { get; }
        IHospitalRepository Hospitals { get; }
        IPatientRepository Patients { get; }
        IPolyclinicRepository Polyclinics { get; }
        IDiagnosticRepository Diagnostics { get; }
        IMedicineRepository Medicines { get; }
        IRecipeRepository Recipes { get; }
        Task<int> SaveAsync();

    }
}
