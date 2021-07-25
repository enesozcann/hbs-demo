using HastaBilgiSistemi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Data.Concrete.EntityFramework.Mappings
{
    public class AppointmentMap : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasOne(a => a.Doctor).WithMany(d => d.Appointments).HasForeignKey(a => a.DoctorId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(a => a.Patient).WithMany(p => p.Appointments).HasForeignKey(a => a.PatientId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            //EntityBase
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();          
            //Table Name
            builder.ToTable("t_appointments");

            builder.HasData(new Appointment
            {
                Id=1,
                AppointmentDate=DateTime.Now,
                PatientId =1,
                DoctorId =1,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            });
        }
    }
}
