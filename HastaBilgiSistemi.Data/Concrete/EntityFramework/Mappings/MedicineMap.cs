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
    public class MedicineMap : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(40);
            builder.Property(m => m.Prospectus).IsRequired().HasMaxLength(540);
            builder.Property(m => m.ATCName).IsRequired().HasMaxLength(60);
            builder.Property(m => m.Company).IsRequired().HasMaxLength(40);
            builder.ToTable("t_medicines");

            //EntityBase
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();
            builder.Property(m => m.CreatedDate).IsRequired();
            builder.Property(m => m.ModifiedDate).IsRequired();
            builder.Property(m => m.IsActive).IsRequired();
            builder.Property(m => m.IsDeleted).IsRequired();

            var medicine1 = new Medicine
            {
                Id = 1,
                Name= "CANLOX PLUS",
                Prospectus= "CANLOX PLUS 16 MG/10 MG/12,5 MG TABLET (28 TABLET)",
                ATCName = "candesartan, amlodipine and hydrochlorothiazide",
                Company= "DEVA HOLDİNG A.Ş.",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };
            var medicine2 = new Medicine
            {
                Id = 2,
                Name = "TRAVOGEN",
                Prospectus = "TRAVOGEN %1 KREM (30 G)",
                ATCName = "isoconazole",
                Company = "ABDİ İBRAHİM",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };
            var medicine3 = new Medicine
            {
                Id = 3,
                Name = "ZOLTEM",
                Prospectus = "ZOLTEM 4 MG 6 FILM TABLET",
                ATCName = "ondansetron",
                Company = "NOBEL İLAÇ SAN. VE TİC. A.Ş.",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };
            builder.HasData(medicine1, medicine2, medicine3);
        }
    }
}
