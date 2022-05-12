using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Findmaster.Migrations
{
    public partial class fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    VacancyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    VacancyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UserPasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    UserPasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Users_Info",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    UserSurname = table.Column<string>(type: "text", nullable: true),
                    UserNumber = table.Column<string>(type: "text", nullable: true),
                    UserAddress = table.Column<string>(type: "text", nullable: true),
                    UserBirthday = table.Column<string>(type: "text", nullable: true),
                    UserGender = table.Column<bool>(type: "boolean", nullable: false),
                    UserSkills = table.Column<string>(type: "text", nullable: true),
                    UserWorkexp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Info", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Users_Type",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserType = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Type", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "vacancies",
                columns: table => new
                {
                    VacancyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vacancy_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VacancySalary = table.Column<int>(type: "integer", nullable: false),
                    vacancy_employername = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    vacancy_address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    vacancy_requirements = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    vacancy_experience = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    vacancy_employment_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    vacancy_description = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacancies", x => x.VacancyId);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessagesId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    message_text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    FromUserId = table.Column<int>(type: "integer", nullable: false),
                    ToUserId = table.Column<int>(type: "integer", nullable: false),
                    VacancyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessagesId);
                    table.ForeignKey(
                        name: "FK_Messages_vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "vacancies",
                        principalColumn: "VacancyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_VacancyId",
                table: "Messages",
                column: "VacancyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "Users_Info");

            migrationBuilder.DropTable(
                name: "Users_Type");

            migrationBuilder.DropTable(
                name: "vacancies");
        }
    }
}
