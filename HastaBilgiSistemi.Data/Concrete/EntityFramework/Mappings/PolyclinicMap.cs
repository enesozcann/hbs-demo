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
    public class PolyclinicMap : IEntityTypeConfiguration<Polyclinic>
    {
        public void Configure(EntityTypeBuilder<Polyclinic> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(80);
            builder.HasOne<Hospital>(p => p.Hospital).WithMany(h => h.Policlinics).HasForeignKey(p => p.HospitalId);

            //Entity Base
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.IsDeleted).IsRequired();

            //Table Name
            builder.ToTable("t_polyclinics");

            builder.HasData(new Polyclinic
            {
                Id = 1,
                Name = "K.B.B Polikliniği",
                HospitalId = 1,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsDeleted = false,
                IsActive = true
            });
        }
    }
}
