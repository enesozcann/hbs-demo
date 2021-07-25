using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HastaBilgiSistemi.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_hospital",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(360)", maxLength: 360, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_hospital", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Prospectus = table.Column<string>(type: "nvarchar(540)", maxLength: 540, nullable: false),
                    ATCName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_polyclinics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_polyclinics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_polyclinics_t_hospital_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "t_hospital",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_role_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_role_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_role_claims_t_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "t_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IdentityNumber = table.Column<long>(type: "BIGINT", maxLength: 11, nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<short>(type: "SMALLINT", nullable: false),
                    Height = table.Column<byte>(type: "TINYINT", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_patients_t_users_UserId",
                        column: x => x.UserId,
                        principalTable: "t_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_user_claims_t_users_UserId",
                        column: x => x.UserId,
                        principalTable: "t_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_logins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_t_user_logins_t_users_UserId",
                        column: x => x.UserId,
                        principalTable: "t_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_roles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_roles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_t_user_roles_t_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "t_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_user_roles_t_users_UserId",
                        column: x => x.UserId,
                        principalTable: "t_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_tokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_t_user_tokens_t_users_UserId",
                        column: x => x.UserId,
                        principalTable: "t_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PoliclinicId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_doctors_t_polyclinics_PoliclinicId",
                        column: x => x.PoliclinicId,
                        principalTable: "t_polyclinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_doctors_t_users_UserId",
                        column: x => x.UserId,
                        principalTable: "t_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_appointments_t_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "t_doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_t_appointments_t_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "t_patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "t_diagnostics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_diagnostics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_diagnostics_t_appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "t_appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_diagnostics_t_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "t_doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_t_diagnostics_t_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "t_patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "t_recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagnosticId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_recipes_t_diagnostics_DiagnosticId",
                        column: x => x.DiagnosticId,
                        principalTable: "t_diagnostics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_recipes_t_medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "t_medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "t_hospital",
                columns: new[] { "Id", "Address", "CreatedDate", "IsActive", "IsDeleted", "ModifiedDate", "Name" },
                values: new object[] { 1, "Bafra/Samsun", new DateTime(2021, 5, 27, 23, 37, 21, 662, DateTimeKind.Local).AddTicks(9025), true, false, new DateTime(2021, 5, 27, 23, 37, 21, 662, DateTimeKind.Local).AddTicks(9038), "Bafra Devlet Hastanesi" });

            migrationBuilder.InsertData(
                table: "t_medicines",
                columns: new[] { "Id", "ATCName", "Company", "CreatedDate", "IsActive", "IsDeleted", "ModifiedDate", "Name", "Prospectus" },
                values: new object[,]
                {
                    { 1, "candesartan, amlodipine and hydrochlorothiazide", "DEVA HOLDİNG A.Ş.", new DateTime(2021, 5, 27, 23, 37, 21, 722, DateTimeKind.Local).AddTicks(3989), true, false, new DateTime(2021, 5, 27, 23, 37, 21, 722, DateTimeKind.Local).AddTicks(4004), "CANLOX PLUS", "CANLOX PLUS 16 MG/10 MG/12,5 MG TABLET (28 TABLET)" },
                    { 2, "isoconazole", "ABDİ İBRAHİM", new DateTime(2021, 5, 27, 23, 37, 21, 722, DateTimeKind.Local).AddTicks(4019), true, false, new DateTime(2021, 5, 27, 23, 37, 21, 722, DateTimeKind.Local).AddTicks(4020), "TRAVOGEN", "TRAVOGEN %1 KREM (30 G)" },
                    { 3, "ondansetron", "NOBEL İLAÇ SAN. VE TİC. A.Ş.", new DateTime(2021, 5, 27, 23, 37, 21, 722, DateTimeKind.Local).AddTicks(4024), true, false, new DateTime(2021, 5, 27, 23, 37, 21, 722, DateTimeKind.Local).AddTicks(4025), "ZOLTEM", "ZOLTEM 4 MG 6 FILM TABLET" }
                });

            migrationBuilder.InsertData(
                table: "t_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "5a1a2a8a-a6f6-46f2-b63c-965c2b53349d", "Admin", "ADMIN" },
                    { 2, "abe09f93-a98f-4373-82c0-9ff3aadc6e0b", "Doctor", "DOCTOR" },
                    { 3, "6d4ea9e6-94a0-4e45-887c-716945622e59", "Patient", "PATIENT" }
                });

            migrationBuilder.InsertData(
                table: "t_users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "950160cd-7392-4404-9647-0459770550ab", "admin@hbs.com", true, "Enes", "Özcan", false, null, "ADMIN@HBS.COM", "ADMIN", "AQAAAAEAACcQAAAAEMEcuqbbgUJ8nQbeId9dcRq9ZTIxhaEB7iCamZyqNQHQCiIbC0FKUV3tHMu5TwWoYg==", "+905555555555", true, "defaultUser.png", "e0c2ca5e-2dfa-4de9-a104-2bec9ea5b1a3", false, "admin" },
                    { 2, 0, "a3f519e6-67fe-4f3d-980b-b9e3d653f8c3", "doctor@hbs.com", true, "İlayda", "Özcan", false, null, "DOCTOR@HBS.COM", "DOCTOR", "AQAAAAEAACcQAAAAEKxF1Gb85IiB3/8MCz31BbWvUIWgepbb2+xTlGNJiDTK44LGNiNCwDxKrrDEYI4+og==", "+905555555555", true, "defaultUser.png", "a23cdec7-26ba-4867-9af9-7e2822f49166", false, "doctor" },
                    { 3, 0, "8b2fab48-f1fa-4f14-bb1f-68b0baabe1b6", "patient@hbs.com", true, "İbrahim", "Dursun", false, null, "PATIENT@HBS.COM", "PATIENT", "AQAAAAEAACcQAAAAEPyqbKagrEIc7Wg/2vXLl4ot7wn2kx82bLE0ug4OpS+s7ZQatr0nCKXI1/Ux1zZGNw==", "+905555555555", true, "defaultUser.png", "e506e6ae-00e5-44fc-a05f-dd6052df42ac", false, "patient" }
                });

            migrationBuilder.InsertData(
                table: "t_patients",
                columns: new[] { "Id", "Address", "BirthDay", "CreatedDate", "Height", "IdentityNumber", "IsActive", "IsDeleted", "ModifiedDate", "UserId", "Weight" },
                values: new object[] { 1, "Bafra/Samsun", new DateTime(2021, 5, 27, 23, 37, 21, 715, DateTimeKind.Local).AddTicks(6150), new DateTime(2021, 5, 27, 23, 37, 21, 715, DateTimeKind.Local).AddTicks(7780), (byte)170, 35648954110L, true, false, new DateTime(2021, 5, 27, 23, 37, 21, 715, DateTimeKind.Local).AddTicks(7795), 3, (short)80 });

            migrationBuilder.InsertData(
                table: "t_polyclinics",
                columns: new[] { "Id", "CreatedDate", "HospitalId", "IsActive", "IsDeleted", "ModifiedDate", "Name" },
                values: new object[] { 1, new DateTime(2021, 5, 27, 23, 37, 21, 660, DateTimeKind.Local).AddTicks(7455), 1, true, false, new DateTime(2021, 5, 27, 23, 37, 21, 660, DateTimeKind.Local).AddTicks(7472), "K.B.B Polikliniği" });

            migrationBuilder.InsertData(
                table: "t_user_roles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "t_doctors",
                columns: new[] { "Id", "CreatedDate", "IsActive", "IsDeleted", "ModifiedDate", "PoliclinicId", "UserId" },
                values: new object[] { 1, new DateTime(2021, 5, 27, 23, 37, 21, 717, DateTimeKind.Local).AddTicks(1258), true, false, new DateTime(2021, 5, 27, 23, 37, 21, 717, DateTimeKind.Local).AddTicks(1272), 1, 2 });

            migrationBuilder.InsertData(
                table: "t_appointments",
                columns: new[] { "Id", "AppointmentDate", "CreatedDate", "DoctorId", "IsActive", "IsDeleted", "ModifiedDate", "PatientId" },
                values: new object[] { 1, new DateTime(2021, 5, 27, 23, 37, 21, 653, DateTimeKind.Local).AddTicks(2563), new DateTime(2021, 5, 27, 23, 37, 21, 653, DateTimeKind.Local).AddTicks(5027), 1, true, false, new DateTime(2021, 5, 27, 23, 37, 21, 653, DateTimeKind.Local).AddTicks(5570), 1 });

            migrationBuilder.InsertData(
                table: "t_diagnostics",
                columns: new[] { "Id", "AppointmentId", "CreatedDate", "Detail", "DoctorId", "IsActive", "IsDeleted", "ModifiedDate", "Name", "PatientId" },
                values: new object[] { 1, 1, new DateTime(2021, 5, 27, 23, 37, 21, 720, DateTimeKind.Local).AddTicks(4684), "Kol Burkulması", 1, true, false, new DateTime(2021, 5, 27, 23, 37, 21, 720, DateTimeKind.Local).AddTicks(4699), "Eklem Burkulması", 1 });

            migrationBuilder.InsertData(
                table: "t_recipes",
                columns: new[] { "Id", "CreatedDate", "DiagnosticId", "IsDeleted", "MedicineId" },
                values: new object[] { 1, new DateTime(2021, 5, 27, 23, 37, 21, 724, DateTimeKind.Local).AddTicks(2353), 1, false, 1 });

            migrationBuilder.InsertData(
                table: "t_recipes",
                columns: new[] { "Id", "CreatedDate", "DiagnosticId", "IsDeleted", "MedicineId" },
                values: new object[] { 2, new DateTime(2021, 5, 27, 23, 37, 21, 724, DateTimeKind.Local).AddTicks(3261), 1, false, 2 });

            migrationBuilder.InsertData(
                table: "t_recipes",
                columns: new[] { "Id", "CreatedDate", "DiagnosticId", "IsDeleted", "MedicineId" },
                values: new object[] { 3, new DateTime(2021, 5, 27, 23, 37, 21, 724, DateTimeKind.Local).AddTicks(3266), 1, false, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_t_appointments_DoctorId",
                table: "t_appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_t_appointments_PatientId",
                table: "t_appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_t_diagnostics_AppointmentId",
                table: "t_diagnostics",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_t_diagnostics_DoctorId",
                table: "t_diagnostics",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_t_diagnostics_PatientId",
                table: "t_diagnostics",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_t_doctors_PoliclinicId",
                table: "t_doctors",
                column: "PoliclinicId");

            migrationBuilder.CreateIndex(
                name: "IX_t_doctors_UserId",
                table: "t_doctors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_patients_IdentityNumber",
                table: "t_patients",
                column: "IdentityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_patients_UserId",
                table: "t_patients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_polyclinics_HospitalId",
                table: "t_polyclinics",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_t_recipes_DiagnosticId",
                table: "t_recipes",
                column: "DiagnosticId");

            migrationBuilder.CreateIndex(
                name: "IX_t_recipes_MedicineId",
                table: "t_recipes",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_t_role_claims_RoleId",
                table: "t_role_claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "t_roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_claims_UserId",
                table: "t_user_claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_logins_UserId",
                table: "t_user_logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_roles_RoleId",
                table: "t_user_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "t_users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "t_users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_recipes");

            migrationBuilder.DropTable(
                name: "t_role_claims");

            migrationBuilder.DropTable(
                name: "t_user_claims");

            migrationBuilder.DropTable(
                name: "t_user_logins");

            migrationBuilder.DropTable(
                name: "t_user_roles");

            migrationBuilder.DropTable(
                name: "t_user_tokens");

            migrationBuilder.DropTable(
                name: "t_diagnostics");

            migrationBuilder.DropTable(
                name: "t_medicines");

            migrationBuilder.DropTable(
                name: "t_roles");

            migrationBuilder.DropTable(
                name: "t_appointments");

            migrationBuilder.DropTable(
                name: "t_doctors");

            migrationBuilder.DropTable(
                name: "t_patients");

            migrationBuilder.DropTable(
                name: "t_polyclinics");

            migrationBuilder.DropTable(
                name: "t_users");

            migrationBuilder.DropTable(
                name: "t_hospital");
        }
    }
}
