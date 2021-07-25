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
    public class DiagnosticMap : IEntityTypeConfiguration<Diagnostic>
    {
        public void Configure(EntityTypeBuilder<Diagnostic> builder)
        {
            builder.HasOne(d => d.Appointment).WithMany(a => a.Diagnostics).HasForeignKey(d => d.AppointmentId).IsRequired();
            builder.HasOne(d => d.Doctor).WithMany(d => d.Diagnostics).HasForeignKey(d => d.DoctorId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(d => d.Patient).WithMany(p => p.Diagnostics).HasForeignKey(d => d.PatientId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.ToTable("t_diagnostics");



            builder.Property(d => d.Detail).IsRequired().HasMaxLength(140);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(30);

            //EntityBase
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();
            builder.Property(d => d.CreatedDate).IsRequired();
            builder.Property(d => d.ModifiedDate).IsRequired();
            builder.Property(d => d.IsActive).IsRequired();
            builder.Property(d => d.IsDeleted).IsRequired();

            builder.HasData(new Diagnostic
            {
                Id = 1,
                Name="Eklem Burkulması",
                Detail="Kol Burkulması",
                PatientId = 1,
                DoctorId = 1,
                AppointmentId=1,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            });
        }
    }
}
