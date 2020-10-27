using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieKolumnyNazwaWTabeliKlienci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nazwa",
                table: "Klienci",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nazwa",
                table: "Klienci");
        }
    }
}
