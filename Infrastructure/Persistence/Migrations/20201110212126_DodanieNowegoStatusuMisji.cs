using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieNowegoStatusuMisji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StatusyMisji",
                columns: new[] { "Id", "Nazwa" },
                values: new object[] { new Guid("b59173ac-0606-47c4-9ddf-0af363d564de"), "Utworzona" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusyMisji",
                keyColumn: "Id",
                keyValue: new Guid("b59173ac-0606-47c4-9ddf-0af363d564de"));
        }
    }
}
