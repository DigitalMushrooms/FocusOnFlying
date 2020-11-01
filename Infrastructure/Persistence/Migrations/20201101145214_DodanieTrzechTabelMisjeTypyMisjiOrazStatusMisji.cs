using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieTrzechTabelMisjeTypyMisjiOrazStatusMisji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusyMisji",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nazwa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusyMisji", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypyMisji",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nazwa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypyMisji", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Misje",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nazwa = table.Column<string>(nullable: false),
                    Opis = table.Column<string>(nullable: false),
                    IdTypuMisji = table.Column<Guid>(nullable: false),
                    MaksymalnaWysokoscLotu = table.Column<int>(nullable: false),
                    IdStatusuMisji = table.Column<Guid>(nullable: false),
                    DataRozpoczecia = table.Column<DateTime>(nullable: false),
                    DataZakonczenia = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Misje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Misje_StatusyMisji_IdStatusuMisji",
                        column: x => x.IdStatusuMisji,
                        principalTable: "StatusyMisji",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Misje_TypyMisji_IdTypuMisji",
                        column: x => x.IdTypuMisji,
                        principalTable: "TypyMisji",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StatusyMisji",
                columns: new[] { "Id", "Nazwa" },
                values: new object[,]
                {
                    { new Guid("beb37d80-9eeb-483c-ab5b-a95c30f6f1ce"), "Zaplanowana" },
                    { new Guid("c3505f48-abd3-43f8-98cf-439129cc4194"), "Anulowana" },
                    { new Guid("34e560c8-d677-4292-aaaf-af1fd9d4e8e0"), "Wykonana" }
                });

            migrationBuilder.InsertData(
                table: "TypyMisji",
                columns: new[] { "Id", "Nazwa" },
                values: new object[,]
                {
                    { new Guid("ed9994f8-c9c8-4781-8423-67bcf005064f"), "BVLOS" },
                    { new Guid("f9a094e4-c4ae-492d-9af4-966022b156d9"), "VLOS" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Misje_IdStatusuMisji",
                table: "Misje",
                column: "IdStatusuMisji",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Misje_IdTypuMisji",
                table: "Misje",
                column: "IdTypuMisji",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Misje");

            migrationBuilder.DropTable(
                name: "StatusyMisji");

            migrationBuilder.DropTable(
                name: "TypyMisji");
        }
    }
}
