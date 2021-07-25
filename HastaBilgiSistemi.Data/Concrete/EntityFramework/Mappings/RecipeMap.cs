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
    public class RecipeMap : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasOne(r => r.Diagnostic).WithMany(d => d.Recipes).HasForeignKey(r => r.DiagnosticId).IsRequired();
            builder.HasOne(r => r.Medicine).WithMany(m => m.Recipes).HasForeignKey(r => r.MedicineId).IsRequired();
            builder.ToTable("t_recipes");

            //EntityBase
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.CreatedDate).IsRequired();
            builder.Property(r => r.IsDeleted).IsRequired();

            var recipe1 = new Recipe
            {
                Id = 1,
                DiagnosticId=1,
                MedicineId=1,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            var recipe2 = new Recipe
            {
                Id = 2,
                DiagnosticId = 1,
                MedicineId = 2,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            var recipe3 = new Recipe
            {
                Id = 3,
                DiagnosticId = 1,
                MedicineId = 3,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            builder.HasData(recipe1, recipe2, recipe3);
        }
    }
}
