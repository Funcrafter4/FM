using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Findmaster.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Favourites",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "Favourites",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Applications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "Applications",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "Applications");
        }
    }
}
