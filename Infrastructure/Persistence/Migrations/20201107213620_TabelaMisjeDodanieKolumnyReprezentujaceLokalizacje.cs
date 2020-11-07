using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class TabelaMisjeDodanieKolumnyReprezentujaceLokalizacje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DlugoscGeograficzna",
                table: "Misje",
                type: "decimal(9,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NazwaLokalizacji",
                table: "Misje",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Promien",
                table: "Misje",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SzerokoscGeograficzna",
                table: "Misje",
                type: "decimal(8,6)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DlugoscGeograficzna",
                table: "Misje");

            migrationBuilder.DropColumn(
                name: "NazwaLokalizacji",
                table: "Misje");

            migrationBuilder.DropColumn(
                name: "Promien",
                table: "Misje");

            migrationBuilder.DropColumn(
                name: "SzerokoscGeograficzna",
                table: "Misje");
        }
    }
}
