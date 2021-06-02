using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Uplata_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uplata",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(nullable: false),
                    Iznos = table.Column<int>(nullable: false),
                    PrijavaId = table.Column<int>(nullable: true),
                    DogadjajId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uplata_Dogadjaj_DogadjajId",
                        column: x => x.DogadjajId,
                        principalTable: "Dogadjaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uplata_Prijava_PrijavaId",
                        column: x => x.PrijavaId,
                        principalTable: "Prijava",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uplata_DogadjajId",
                table: "Uplata",
                column: "DogadjajId");

            migrationBuilder.CreateIndex(
                name: "IX_Uplata_PrijavaId",
                table: "Uplata",
                column: "PrijavaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uplata");
        }
    }
}
