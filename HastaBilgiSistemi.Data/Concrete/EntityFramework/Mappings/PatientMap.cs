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
    public class PatientMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(p => p.IdentityNumber)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnType("BIGINT");
            builder.HasIndex(p => p.IdentityNumber).IsUnique();
            builder.Property(p => p.BirthDay).IsRequired();
            builder.Property(p => p.Weight).HasColumnType("SMALLINT");
            builder.Property(p => p.Height).HasColumnType("TINYINT");
            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(180);
            //Table Name
            builder.ToTable("t_patients");

            builder.HasOne(d => d.User).WithOne(u => u.Patient).HasForeignKey<Patient>(d => d.UserId);

            builder.HasData(new Patient
            {
                Id = 1,
                UserId = 3,
                IdentityNumber = 35648954110,
                BirthDay = DateTime.Now,
                Weight = 80,
                Height = 170,
                Address = "Bafra/Samsun",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            });
        }
    }
}
