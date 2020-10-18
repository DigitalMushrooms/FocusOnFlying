using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class UsuniecieKolumnZTabeliKlienci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dzielnica",
                table: "Klienci");

            migrationBuilder.DropColumn(
                name: "Gmina",
                table: "Klienci");

            migrationBuilder.DropColumn(
                name: "ZagranicznyKodPocztowy",
                table: "Klienci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dzielnica",
                table: "Klienci",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gmina",
                table: "Klienci",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZagranicznyKodPocztowy",
                table: "Klienci",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }
    }
}
