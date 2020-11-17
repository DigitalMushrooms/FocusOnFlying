using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieDoTabeliUslugiKluczaObcegoIdStatusuUslugi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdStatusuUslugi",
                table: "Uslugi",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "StatusyUslugi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nazwa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusyUslugi", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "StatusyUslugi",
                columns: new[] { "Id", "Nazwa" },
                values: new object[,]
                {
                    { new Guid("89407a86-a6d6-415a-b3bf-d3ee0b70ac85"), "Utworzona" },
                    { new Guid("ee545a45-a7ed-4aa9-9ac6-def05c93204f"), "W realizacji" },
                    { new Guid("eef8529f-9182-434b-957c-2df7462e2fbf"), "Zakończona" },
                    { new Guid("bdb1da1b-3713-46a9-8414-1c9a2e91f931"), "Anulowana" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uslugi_IdStatusuUslugi",
                table: "Uslugi",
                column: "IdStatusuUslugi");

            migrationBuilder.AddForeignKey(
                name: "FK_Uslugi_StatusyUslugi_IdStatusuUslugi",
                table: "Uslugi",
                column: "IdStatusuUslugi",
                principalTable: "StatusyUslugi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uslugi_StatusyUslugi_IdStatusuUslugi",
                table: "Uslugi");

            migrationBuilder.DropTable(
                name: "StatusyUslugi");

            migrationBuilder.DropIndex(
                name: "IX_Uslugi_IdStatusuUslugi",
                table: "Uslugi");

            migrationBuilder.DropColumn(
                name: "IdStatusuUslugi",
                table: "Uslugi");
        }
    }
}
