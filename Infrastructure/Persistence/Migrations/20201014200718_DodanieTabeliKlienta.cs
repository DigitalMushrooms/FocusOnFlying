using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieTabeliKlienta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klienci",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Imie = table.Column<string>(maxLength: 100, nullable: false),
                    Nazwisko = table.Column<string>(maxLength: 100, nullable: false),
                    Pesel = table.Column<string>(maxLength: 11, nullable: true),
                    Regon = table.Column<string>(maxLength: 14, nullable: true),
                    Nip = table.Column<string>(maxLength: 10, nullable: true),
                    NumerPaszportu = table.Column<string>(maxLength: 50, nullable: true),
                    NumerTelefonu = table.Column<string>(maxLength: 20, nullable: false),
                    KodPocztowy = table.Column<string>(maxLength: 20, nullable: true),
                    Miejscowosc = table.Column<string>(maxLength: 100, nullable: false),
                    Gmina = table.Column<string>(maxLength: 100, nullable: false),
                    Dzielnica = table.Column<string>(maxLength: 100, nullable: false),
                    Ulica = table.Column<string>(maxLength: 100, nullable: false),
                    NumerDomu = table.Column<string>(maxLength: 10, nullable: false),
                    NumerLokalu = table.Column<string>(maxLength: 10, nullable: false),
                    SymbolPanstwa = table.Column<string>(maxLength: 3, nullable: false),
                    ZagranicznyKodPocztowy = table.Column<string>(maxLength: 3, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienci", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Klienci");
        }
    }
}
