using AutoMapper;
using HastaBilgiSistemi.Data.Abstract;
using HastaBilgiSistemi.Data.Concrete;
using HastaBilgiSistemi.Data.Concrete.EntityFramework.Contexts;
using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Services.Abstract;
using HastaBilgiSistemi.Services.AutoMapper.Profiles;
using HastaBilgiSistemi.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection, string connectionString)
        {
            //serviceCollection.AddDbContext<HBSContexts>();
            serviceCollection.AddDbContext<HBSContexts>(options => options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            serviceCollection.AddIdentity<User, Role>(options=> 
            {
                // Kullanıcı Parola Seçenekleri
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 12;
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                // Kullanıcı Username ve Mail Seçenekleri
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<HBSContexts>();
            serviceCollection.AddScoped<IAppointmentService, AppointmentManager>();
            serviceCollection.AddScoped<IPolyclinicService, PolyclinicManager>();
            serviceCollection.AddScoped<IHospitalService, HospitalManager>();
            serviceCollection.AddScoped<IPatientService, PatientManager>();
            serviceCollection.AddScoped<IDoctorService, DoctorManager>();
            serviceCollection.AddScoped<IDiagnosticService, DiagnosticManager>();
            serviceCollection.AddScoped<IRecipeService, RecipeManager>();
            serviceCollection.AddScoped<IMedicineService, MedicineManager>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            return serviceCollection;
        }
    }
}
