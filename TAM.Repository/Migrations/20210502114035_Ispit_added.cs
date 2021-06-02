using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Ispit_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ispit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    VrijemePocetka = table.Column<DateTime>(nullable: false),
                    VrijemeZavrsetka = table.Column<DateTime>(nullable: false),
                    UrlDokumenta = table.Column<string>(nullable: true),
                    OrganizacijaKursaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ispit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ispit_OrganizacijaKursa_OrganizacijaKursaId",
                        column: x => x.OrganizacijaKursaId,
                        principalTable: "OrganizacijaKursa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.CreateIndex(
                name: "IX_Ispit_OrganizacijaKursaId",
                table: "Ispit",
                column: "OrganizacijaKursaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ispit");
        }
    }
}
