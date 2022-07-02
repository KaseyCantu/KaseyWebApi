using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KaseyWebApi.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NationalIdNumber = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: true),
                    EmployeeName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    LoginId = table.Column<string>(type: "character varying(256)", unicode: false, maxLength: 256, nullable: true),
                    JobTitle = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", unicode: false, nullable: false),
                    MaritalStatus = table.Column<string>(type: "character varying(1)", unicode: false, maxLength: 1, nullable: true),
                    Gender = table.Column<string>(type: "character varying(1)", unicode: false, maxLength: 1, nullable: true),
                    HireDate = table.Column<DateTime>(type: "timestamp with time zone", unicode: false, nullable: false),
                    VacationHours = table.Column<short>(type: "smallint", unicode: false, nullable: false),
                    SickLeaveHours = table.Column<short>(type: "smallint", unicode: false, nullable: false),
                    RowGuid = table.Column<Guid>(type: "uuid", unicode: false, maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "links",
                columns: table => new
                {
                    LinkId = table.Column<string>(type: "text", nullable: false),
                    LinkUrl = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", unicode: false, maxLength: 250, nullable: false),
                    Topic = table.Column<string>(type: "text", unicode: false, nullable: false),
                    CreatedAt = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_links", x => x.LinkId);
                });

            migrationBuilder.CreateTable(
                name: "userInfo",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisplayName = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true),
                    UserName = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userInfo", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "EmployeeId", "BirthDate", "EmployeeName", "Gender", "HireDate", "JobTitle", "LoginId", "MaritalStatus", "ModifiedDate", "NationalIdNumber", "RowGuid", "SickLeaveHours", "VacationHours" },
                values: new object[] { 9, new DateTime(1993, 6, 22, 5, 0, 0, 0, DateTimeKind.Utc), "Kasey Cantu", "M", new DateTime(2018, 8, 9, 5, 0, 0, 0, DateTimeKind.Utc), "Software Engieer", "shipengine-kasey", "M", new DateTime(2022, 5, 27, 5, 0, 0, 0, DateTimeKind.Utc), "8989898989898989", new Guid("cc8916e7-2d08-48a6-b5a3-ed71412e1346"), (short)300, (short)500 });

            migrationBuilder.InsertData(
                table: "links",
                columns: new[] { "LinkId", "CreatedAt", "Description", "LinkUrl", "Topic" },
                values: new object[] { "d8d854e7-26bc-4df7-a2ed-580c6d6eeb02", "7/2/2022 12:12:21 AM", "Search Engine", "https://google.com", "Learning" });

            migrationBuilder.InsertData(
                table: "userInfo",
                columns: new[] { "UserId", "CreatedDate", "DisplayName", "Email", "Password", "UserName" },
                values: new object[] { 7, new DateTime(2022, 7, 2, 5, 12, 21, 223, DateTimeKind.Utc).AddTicks(8650), "KPC", "kaseypaulcantu@gmail.com", "password321", "KaseyPCantu" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "links");

            migrationBuilder.DropTable(
                name: "userInfo");
        }
    }
}
