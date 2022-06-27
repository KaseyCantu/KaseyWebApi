using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaseyWebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "links",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LinkUrl = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Topic = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_links", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "links",
                columns: new[] { "Id", "CreatedAt", "Description", "LinkUrl", "Topic" },
                values: new object[] { "95a5163a-898d-43c6-bfa1-6216acb22eb6", "6/24/2022 12:44:34 AM", "Search Engine", "https://google.com", "Learning" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "links");
        }
    }
}
