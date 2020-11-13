using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieKluczaObcegoWTabeliMisje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdUslugi",
                table: "Misje",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Misje_IdUslugi",
                table: "Misje",
                column: "IdUslugi");

            migrationBuilder.AddForeignKey(
                name: "FK_Misje_Uslugi_IdUslugi",
                table: "Misje",
                column: "IdUslugi",
                principalTable: "Uslugi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Misje_Uslugi_IdUslugi",
                table: "Misje");

            migrationBuilder.DropIndex(
                name: "IX_Misje_IdUslugi",
                table: "Misje");

            migrationBuilder.DropColumn(
                name: "IdUslugi",
                table: "Misje");
        }
    }
}
