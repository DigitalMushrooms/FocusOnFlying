using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieTabeliAudyt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audyt",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdAudytowanegoWiersza = table.Column<Guid>(nullable: false),
                    NazwaTabeli = table.Column<string>(nullable: false),
                    Dane = table.Column<string>(nullable: false),
                    DataAudytu = table.Column<DateTime>(nullable: false),
                    Uzytkownik = table.Column<string>(nullable: false),
                    TypOperacji = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audyt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audyt");
        }
    }
}
