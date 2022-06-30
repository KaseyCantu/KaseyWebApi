using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaseyWebApi.Migrations
{
    public partial class UpdateEmployeeDbName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "links",
                keyColumn: "Id",
                keyValue: "5c1ee9c7-3174-4d2b-8980-4643e3714379");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "employees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employees",
                table: "employees",
                column: "EmployeeId");

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "EmployeeId",
                keyValue: 9,
                column: "RowGuid",
                value: new Guid("46d9d0fb-0310-4723-9d03-90f6787b4303"));

            migrationBuilder.InsertData(
                table: "links",
                columns: new[] { "Id", "CreatedAt", "Description", "LinkUrl", "Topic" },
                values: new object[] { "d16775b0-854d-470c-ad03-f2f6f14262d0", "6/29/2022 3:33:05 PM", "Search Engine", "https://google.com", "Learning" });

            migrationBuilder.UpdateData(
                table: "userInfo",
                keyColumn: "UserId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2022, 6, 29, 20, 33, 5, 91, DateTimeKind.Utc).AddTicks(5250));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_employees",
                table: "employees");

            migrationBuilder.DeleteData(
                table: "links",
                keyColumn: "Id",
                keyValue: "d16775b0-854d-470c-ad03-f2f6f14262d0");

            migrationBuilder.RenameTable(
                name: "employees",
                newName: "Employees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "EmployeeId");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 9,
                column: "RowGuid",
                value: new Guid("31001f30-95a6-4e1c-8004-ea7521c2323a"));

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
    }
}
