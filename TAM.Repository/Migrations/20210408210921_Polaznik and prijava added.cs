using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Polaznikandprijavaadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Polaznik",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TipPolaznikaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polaznik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polaznik_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Polaznik_TipPolaznika_TipPolaznikaId",
                        column: x => x.TipPolaznikaId,
                        principalTable: "TipPolaznika",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Prijava",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(nullable: false),
                    KursId = table.Column<int>(nullable: false),
                    PolaznikId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prijava", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prijava_Kurs_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Prijava_Polaznik_PolaznikId",
                        column: x => x.PolaznikId,
                        principalTable: "Polaznik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Polaznik_TipPolaznikaId",
                table: "Polaznik",
                column: "TipPolaznikaId");

            migrationBuilder.CreateIndex(
                name: "IX_Prijava_KursId",
                table: "Prijava",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_Prijava_PolaznikId",
                table: "Prijava",
                column: "PolaznikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prijava");

            migrationBuilder.DropTable(
                name: "Polaznik");
        }
    }
}
