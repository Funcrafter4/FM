using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Findmaster.Migrations
{
    public partial class MessagesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_vacancies_VacancyId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_VacancyId",
                table: "Messages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Messages_VacancyId",
                table: "Messages",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_vacancies_VacancyId",
                table: "Messages",
                column: "VacancyId",
                principalTable: "vacancies",
                principalColumn: "VacancyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
