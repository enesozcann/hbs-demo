using HastaBilgiSistemi.Data.Concrete.EntityFramework.Mappings;
using HastaBilgiSistemi.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Data.Concrete.EntityFramework.Contexts
{
    public class HBSContexts:IdentityDbContext<User,Role,int,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Polyclinic> Policlinics { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Diagnostic> Diagnostics { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public HBSContexts(DbContextOptions<HBSContexts> options) : base(options)
        {

        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=localhost;Database=master;Trusted_Connection=True;");
        }*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentMap());
            modelBuilder.ApplyConfiguration(new PolyclinicMap());
            modelBuilder.ApplyConfiguration(new HospitalMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserClaimMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new RoleClaimMap());
            modelBuilder.ApplyConfiguration(new PatientMap());
            modelBuilder.ApplyConfiguration(new DoctorMap());
            modelBuilder.ApplyConfiguration(new DiagnosticMap());
            modelBuilder.ApplyConfiguration(new MedicineMap());
            modelBuilder.ApplyConfiguration(new RecipeMap());
        }
    }
}
