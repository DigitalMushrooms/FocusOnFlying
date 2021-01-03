using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieTabeliFaktury : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdFaktury",
                table: "Uslugi",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Faktury",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NumerFaktury = table.Column<string>(nullable: false),
                    WartoscNetto = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    WartoscBrutto = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    ZaplaconaFaktura = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faktury", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uslugi_IdFaktury",
                table: "Uslugi",
                column: "IdFaktury",
                unique: true,
                filter: "[IdFaktury] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Uslugi_Faktury_IdFaktury",
                table: "Uslugi",
                column: "IdFaktury",
                principalTable: "Faktury",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uslugi_Faktury_IdFaktury",
                table: "Uslugi");

            migrationBuilder.DropTable(
                name: "Faktury");

            migrationBuilder.DropIndex(
                name: "IX_Uslugi_IdFaktury",
                table: "Uslugi");

            migrationBuilder.DropColumn(
                name: "IdFaktury",
                table: "Uslugi");
        }
    }
}
