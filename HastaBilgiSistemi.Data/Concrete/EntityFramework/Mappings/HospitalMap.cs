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
    public class HospitalMap : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.Property(h => h.Name)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(h => h.Address)
                .HasMaxLength(360)
                .IsRequired();

            //Entity Base
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).ValueGeneratedOnAdd();
            builder.Property(h => h.CreatedDate).IsRequired();
            builder.Property(h => h.ModifiedDate).IsRequired();
            builder.Property(h => h.IsActive).IsRequired();
            builder.Property(h => h.IsDeleted).IsRequired();

            //Table Name
            builder.ToTable("t_hospital");

            builder.HasData(new Hospital{
                Id =1,
                Name= "Bafra Devlet Hastanesi",
                Address= "Bafra/Samsun",
                CreatedDate= DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsDeleted = false,
                IsActive = true
            });
        }
    }
}
