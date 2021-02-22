using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class ZmianaNazwyTabeliAudytNaAudyty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Audyt",
                table: "Audyt");

            migrationBuilder.RenameTable(
                name: "Audyt",
                newName: "Audyty");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Audyty",
                table: "Audyty",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Audyty",
                table: "Audyty");

            migrationBuilder.RenameTable(
                name: "Audyty",
                newName: "Audyt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Audyt",
                table: "Audyt",
                column: "Id");
        }
    }
}
