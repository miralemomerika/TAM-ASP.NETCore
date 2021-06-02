using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Radadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumPostavljanja = table.Column<DateTime>(nullable: false),
                    UrlDokumenta = table.Column<string>(nullable: true),
                    IspitId = table.Column<int>(nullable: false),
                    PolaznikId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rad_Ispit_IspitId",
                        column: x => x.IspitId,
                        principalTable: "Ispit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rad_Polaznik_PolaznikId",
                        column: x => x.PolaznikId,
                        principalTable: "Polaznik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rad_IspitId",
                table: "Rad",
                column: "IspitId");

            migrationBuilder.CreateIndex(
                name: "IX_Rad_PolaznikId",
                table: "Rad",
                column: "PolaznikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rad");
        }
    }
}
