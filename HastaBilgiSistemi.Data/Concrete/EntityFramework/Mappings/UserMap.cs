using HastaBilgiSistemi.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Data.Concrete.EntityFramework.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            // Primary key
            b.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

            // Maps to the AspNetUsers table
            b.ToTable("t_users");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.UserName).HasMaxLength(20);
            b.Property(u => u.NormalizedUserName).HasMaxLength(50);
            b.Property(u => u.Email).HasMaxLength(80);
            b.Property(u => u.NormalizedEmail).HasMaxLength(80);
            //my extensions
            b.Property(u => u.FirstName).HasMaxLength(30).IsRequired();
            b.Property(u => u.LastName).HasMaxLength(30).IsRequired();
            b.Property(u => u.Picture).HasMaxLength(250);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            b.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            b.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            b.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();



            var adminUser = new User
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                FirstName = "Enes",
                LastName = "Özcan",
                Email = "admin@hbs.com",
                NormalizedEmail = "ADMIN@HBS.COM",
                PhoneNumber = "+905555555555",
                Picture = "defaultUser.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            adminUser.PasswordHash = CreateHash(adminUser, "admin123456789");
            var doctorUser = new User
            {
                Id = 2,
                UserName = "doctor",
                NormalizedUserName = "DOCTOR",
                FirstName = "İlayda",
                LastName = "Özcan",
                Email = "doctor@hbs.com",
                NormalizedEmail = "DOCTOR@HBS.COM",
                PhoneNumber = "+905555555555",
                Picture = "defaultUser.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            doctorUser.PasswordHash = CreateHash(doctorUser, "doctor123456789");
            var patientUser = new User
            {
                Id = 3,
                UserName = "patient",
                NormalizedUserName = "PATIENT",
                FirstName = "İbrahim",
                LastName = "Dursun",
                Email = "patient@hbs.com",
                NormalizedEmail = "PATIENT@HBS.COM",
                PhoneNumber = "+905555555555",
                Picture = "defaultUser.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            patientUser.PasswordHash = CreateHash(patientUser, "patient123456789");

            b.HasData(adminUser,doctorUser,patientUser);
        }
        private string CreateHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user,password);
        }
    }
}
