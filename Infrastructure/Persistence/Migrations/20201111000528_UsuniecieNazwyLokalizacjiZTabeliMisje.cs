using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class UsuniecieNazwyLokalizacjiZTabeliMisje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NazwaLokalizacji",
                table: "Misje");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NazwaLokalizacji",
                table: "Misje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
