using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieTabeliKraje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SymbolPanstwa",
                table: "Klienci");

            migrationBuilder.AddColumn<Guid>(
                name: "IdKraju",
                table: "Klienci",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Kraje",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NazwaKraju = table.Column<string>(maxLength: 50, nullable: false),
                    Skrot = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kraje", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Kraje",
                columns: new[] { "Id", "NazwaKraju", "Skrot" },
                values: new object[,]
                {
                    { new Guid("abaccb22-4d71-41ef-b96b-199fb007d336"), "Polska", "PL" },
                    { new Guid("ccf2c1c3-56ab-4e12-924c-0e9996ff261f"), "Niemcy", "DE" },
                    { new Guid("d356b661-b7d1-4c75-80c7-677062dfdb94"), "Czechy", "CZ" },
                    { new Guid("2f314b76-9fd4-4e89-ad94-0a4cc9a92f18"), "Słowacja", "SK" },
                    { new Guid("1863424c-f0ee-40e8-b8b7-683a445a8d6e"), "Ukraina", "UA" },
                    { new Guid("be2f6802-38bc-4ac3-abc2-d19714e6689d"), "Białoruś", "BY" },
                    { new Guid("0847f9ab-bd5a-4714-93cb-5a4ec23afee2"), "Litwa", "LV" },
                    { new Guid("b57f5888-24ce-4349-8de8-dcb938678915"), "Rosja", "RU" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Klienci_IdKraju",
                table: "Klienci",
                column: "IdKraju");

            migrationBuilder.CreateIndex(
                name: "IX_Kraje_NazwaKraju",
                table: "Kraje",
                column: "NazwaKraju",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kraje_Skrot",
                table: "Kraje",
                column: "Skrot",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Klienci_Kraje_IdKraju",
                table: "Klienci",
                column: "IdKraju",
                principalTable: "Kraje",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klienci_Kraje_IdKraju",
                table: "Klienci");

            migrationBuilder.DropTable(
                name: "Kraje");

            migrationBuilder.DropIndex(
                name: "IX_Klienci_IdKraju",
                table: "Klienci");

            migrationBuilder.DropColumn(
                name: "IdKraju",
                table: "Klienci");

            migrationBuilder.AddColumn<string>(
                name: "SymbolPanstwa",
                table: "Klienci",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }
    }
}
