using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class odrzananastavaanddolazak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OdrzanaNastava",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumIVrijemeOdrzavanja = table.Column<DateTime>(nullable: false),
                    ProstorijaId = table.Column<int>(nullable: false),
                    OrganizacijaKursaId = table.Column<int>(nullable: false),
                    PredavacId = table.Column<string>(nullable: true),
                    Zakljucen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdrzanaNastava", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdrzanaNastava_OrganizacijaKursa_OrganizacijaKursaId",
                        column: x => x.OrganizacijaKursaId,
                        principalTable: "OrganizacijaKursa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OdrzanaNastava_Predavac_PredavacId",
                        column: x => x.PredavacId,
                        principalTable: "Predavac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OdrzanaNastava_Prostorija_ProstorijaId",
                        column: x => x.ProstorijaId,
                        principalTable: "Prostorija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dolazak",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OdrzanaNastavaId = table.Column<int>(nullable: false),
                    PohadjanjeId = table.Column<int>(nullable: false),
                    Prisutan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dolazak", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dolazak_OdrzanaNastava_OdrzanaNastavaId",
                        column: x => x.OdrzanaNastavaId,
                        principalTable: "OdrzanaNastava",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dolazak_Pohadjanje_PohadjanjeId",
                        column: x => x.PohadjanjeId,
                        principalTable: "Pohadjanje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dolazak_OdrzanaNastavaId",
                table: "Dolazak",
                column: "OdrzanaNastavaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dolazak_PohadjanjeId",
                table: "Dolazak",
                column: "PohadjanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_OdrzanaNastava_OrganizacijaKursaId",
                table: "OdrzanaNastava",
                column: "OrganizacijaKursaId");

            migrationBuilder.CreateIndex(
                name: "IX_OdrzanaNastava_PredavacId",
                table: "OdrzanaNastava",
                column: "PredavacId");

            migrationBuilder.CreateIndex(
                name: "IX_OdrzanaNastava_ProstorijaId",
                table: "OdrzanaNastava",
                column: "ProstorijaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dolazak");

            migrationBuilder.DropTable(
                name: "OdrzanaNastava");
        }
    }
}
