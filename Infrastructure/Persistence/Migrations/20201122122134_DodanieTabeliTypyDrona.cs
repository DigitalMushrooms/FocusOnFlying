using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FocusOnFlying.Infrastructure.Persistence.Migrations
{
    public partial class DodanieTabeliTypyDrona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypyDrona",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nazwa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypyDrona", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TypyDrona",
                columns: new[] { "Id", "Nazwa" },
                values: new object[,]
                {
                    { new Guid("7f4ceb1e-2be1-418d-9c28-2fa11bae2429"), "Aircraft" },
                    { new Guid("686bf52e-798b-42bf-bdfd-3bbd4936d6e8"), "Airship, Balloon" },
                    { new Guid("39e46048-d0f5-479c-aebc-318d06c44d5e"), "Helicopter" },
                    { new Guid("3170d6c9-59c4-486e-b392-61c07cc3a0da"), "Multi Rotor" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypyDrona");
        }
    }
}
