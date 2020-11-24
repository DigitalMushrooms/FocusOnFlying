using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieTabeliDrony : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drony",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Producent = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    NumerSeryjny = table.Column<string>(nullable: false),
                    IdTypuDrona = table.Column<Guid>(nullable: false),
                    DataNastepnegoPrzegladu = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drony", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drony");
        }
    }
}
