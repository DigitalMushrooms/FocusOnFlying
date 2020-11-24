using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieTabeliMisjeDrony : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MisjeDrony",
                columns: table => new
                {
                    IdMisji = table.Column<Guid>(nullable: false),
                    IdDrona = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MisjeDrony", x => new { x.IdMisji, x.IdDrona });
                    table.ForeignKey(
                        name: "FK_MisjeDrony_Drony_IdDrona",
                        column: x => x.IdDrona,
                        principalTable: "Drony",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MisjeDrony_Misje_IdMisji",
                        column: x => x.IdMisji,
                        principalTable: "Misje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MisjeDrony_IdDrona",
                table: "MisjeDrony",
                column: "IdDrona");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MisjeDrony");
        }
    }
}
