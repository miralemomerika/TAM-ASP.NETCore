using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Organizacijakursaadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polaznik_TipPolaznika_TipPolaznikaId",
                table: "Polaznik");

            migrationBuilder.AlterColumn<int>(
                name: "TipPolaznikaId",
                table: "Polaznik",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "OrganizacijaKursa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumPocetka = table.Column<DateTime>(nullable: false),
                    DatumZavrsetka = table.Column<DateTime>(nullable: false),
                    PredavacId = table.Column<string>(nullable: true),
                    KursId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizacijaKursa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizacijaKursa_Kurs_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrganizacijaKursa_Predavac_PredavacId",
                        column: x => x.PredavacId,
                        principalTable: "Predavac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizacijaKursa_KursId",
                table: "OrganizacijaKursa",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizacijaKursa_PredavacId",
                table: "OrganizacijaKursa",
                column: "PredavacId");

            migrationBuilder.AddForeignKey(
                name: "FK_Polaznik_TipPolaznika_TipPolaznikaId",
                table: "Polaznik",
                column: "TipPolaznikaId",
                principalTable: "TipPolaznika",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polaznik_TipPolaznika_TipPolaznikaId",
                table: "Polaznik");

            migrationBuilder.DropTable(
                name: "OrganizacijaKursa");

            migrationBuilder.AlterColumn<int>(
                name: "TipPolaznikaId",
                table: "Polaznik",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Polaznik_TipPolaznika_TipPolaznikaId",
                table: "Polaznik",
                column: "TipPolaznikaId",
                principalTable: "TipPolaznika",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
