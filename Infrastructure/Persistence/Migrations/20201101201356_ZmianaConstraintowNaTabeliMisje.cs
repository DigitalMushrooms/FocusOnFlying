using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class ZmianaConstraintowNaTabeliMisje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Misje_IdStatusuMisji",
                table: "Misje");

            migrationBuilder.DropIndex(
                name: "IX_Misje_IdTypuMisji",
                table: "Misje");

            migrationBuilder.CreateIndex(
                name: "IX_Misje_IdStatusuMisji",
                table: "Misje",
                column: "IdStatusuMisji");

            migrationBuilder.CreateIndex(
                name: "IX_Misje_IdTypuMisji",
                table: "Misje",
                column: "IdTypuMisji");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Misje_IdStatusuMisji",
                table: "Misje");

            migrationBuilder.DropIndex(
                name: "IX_Misje_IdTypuMisji",
                table: "Misje");

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
    }
}
