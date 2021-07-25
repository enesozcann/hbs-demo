using HastaBilgiSistemi.Data.Abstract;
using HastaBilgiSistemi.Data.Concrete.EntityFramework.Contexts;
using HastaBilgiSistemi.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HBSContexts _context;
        private EfAppointmentRepository _appointmentRepository;
        private EfDoctorRepository _doctorRepository;
        private EfHospitalRepository _hospitalRepository;
        private EfPatientRepository _patientRepository;
        private EfPolyclinicRepository _polyclinicRepository;
        private EfDiagnosticRepository _diagnosticRepository;
        private EfMedicineRepository _medicineRepository;
        private EfRecipeRepository _recipeRepository;

        public UnitOfWork(HBSContexts context)
        {
            _context = context;
        }

        public IAppointmentRepository Appointments => _appointmentRepository ??= new EfAppointmentRepository(_context);

        public IDoctorRepository Doctors  => _doctorRepository ??= new EfDoctorRepository(_context);

        public IHospitalRepository Hospitals => _hospitalRepository ??= new EfHospitalRepository(_context);

        public IPatientRepository Patients => _patientRepository ??= new EfPatientRepository(_context);

        public IPolyclinicRepository Polyclinics => _polyclinicRepository ??= new EfPolyclinicRepository(_context);

        public IDiagnosticRepository Diagnostics => _diagnosticRepository ??=new EfDiagnosticRepository(_context);

        public IMedicineRepository Medicines => _medicineRepository ??=new EfMedicineRepository(_context);

        public IRecipeRepository Recipes => _recipeRepository ??=new EfRecipeRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
