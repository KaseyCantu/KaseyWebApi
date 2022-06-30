using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KaseyWebApi.Migrations
{
    public partial class AddedEmployeeSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "links",
                keyColumn: "Id",
                keyValue: "95a0a383-eab3-442e-8dda-336f930426f2");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NationalIdNumber = table.Column<string>(type: "text", nullable: true),
                    EmployeeName = table.Column<string>(type: "text", nullable: true),
                    LoginId = table.Column<string>(type: "text", nullable: true),
                    JobTitle = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaritalStatus = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    HireDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VacationHours = table.Column<short>(type: "smallint", nullable: false),
                    SickLeaveHours = table.Column<short>(type: "smallint", nullable: false),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "BirthDate", "EmployeeName", "Gender", "HireDate", "JobTitle", "LoginId", "MaritalStatus", "ModifiedDate", "NationalIdNumber", "RowGuid", "SickLeaveHours", "VacationHours" },
                values: new object[] { 9, new DateTime(1993, 6, 22, 5, 0, 0, 0, DateTimeKind.Utc), "Kasey Cantu", "M", new DateTime(2018, 8, 9, 5, 0, 0, 0, DateTimeKind.Utc), "Software Engieer", "shipengine-kasey", "M", new DateTime(2022, 5, 27, 5, 0, 0, 0, DateTimeKind.Utc), "8989898989898989", new Guid("31001f30-95a6-4e1c-8004-ea7521c2323a"), (short)300, (short)500 });

            migrationBuilder.InsertData(
                table: "links",
                columns: new[] { "Id", "CreatedAt", "Description", "LinkUrl", "Topic" },
                values: new object[] { "5c1ee9c7-3174-4d2b-8980-4643e3714379", "6/29/2022 3:28:31 PM", "Search Engine", "https://google.com", "Learning" });

            migrationBuilder.UpdateData(
                table: "userInfo",
                keyColumn: "UserId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2022, 6, 29, 20, 28, 31, 446, DateTimeKind.Utc).AddTicks(2480));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DeleteData(
                table: "links",
                keyColumn: "Id",
                keyValue: "5c1ee9c7-3174-4d2b-8980-4643e3714379");

            migrationBuilder.InsertData(
                table: "links",
                columns: new[] { "Id", "CreatedAt", "Description", "LinkUrl", "Topic" },
                values: new object[] { "95a0a383-eab3-442e-8dda-336f930426f2", "6/29/2022 2:56:42 PM", "Search Engine", "https://google.com", "Learning" });

            migrationBuilder.UpdateData(
                table: "userInfo",
                keyColumn: "UserId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2022, 6, 29, 19, 56, 42, 249, DateTimeKind.Utc).AddTicks(3440));
        }
    }
}
