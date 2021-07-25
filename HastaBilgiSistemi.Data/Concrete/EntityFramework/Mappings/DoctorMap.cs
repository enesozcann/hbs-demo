using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Shared.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Data.Concrete.EntityFramework.Mappings
{
    public class DoctorMap : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasOne<Polyclinic>(d => d.Policlinic).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.PoliclinicId); // bir doktorun bir polikliniği var fakat bir poliklinikte birden çok doktor olabilir.        
            //table name
            builder.ToTable("t_doctors");

            //builder.HasOne(d => d.User).WithOne(u => u.Doctor).HasForeignKey<User>(u => u.User);
            builder.HasOne(d => d.User).WithOne(u => u.Doctor).HasForeignKey<Doctor>(d => d.UserId);

            builder.HasData(new Doctor
            {
                Id = 1,
                UserId = 2,
                PoliclinicId =1,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            });
        }
    }
}
