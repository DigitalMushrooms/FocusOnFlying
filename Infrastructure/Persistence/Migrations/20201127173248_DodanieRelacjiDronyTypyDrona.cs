using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieRelacjiDronyTypyDrona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Drony_IdTypuDrona",
                table: "Drony",
                column: "IdTypuDrona");

            migrationBuilder.AddForeignKey(
                name: "FK_Drony_TypyDrona_IdTypuDrona",
                table: "Drony",
                column: "IdTypuDrona",
                principalTable: "TypyDrona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drony_TypyDrona_IdTypuDrona",
                table: "Drony");

            migrationBuilder.DropIndex(
                name: "IX_Drony_IdTypuDrona",
                table: "Drony");
        }
    }
}
