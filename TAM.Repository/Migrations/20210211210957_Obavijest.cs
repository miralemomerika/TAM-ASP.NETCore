using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Obavijest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Kurs",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Obavijest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(maxLength: 30, nullable: false),
                    DatumIVrijeme = table.Column<DateTime>(nullable: false),
                    Sadrzaj = table.Column<string>(nullable: false),
                    KategorijaObavijestiId = table.Column<int>(nullable: false),
                    KorisnickiRacunId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obavijest_KategorijaObavijesti_KategorijaObavijestiId",
                        column: x => x.KategorijaObavijestiId,
                        principalTable: "KategorijaObavijesti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Obavijest_AspNetUsers_KorisnickiRacunId",
                        column: x => x.KorisnickiRacunId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Obavijest_KategorijaObavijestiId",
                table: "Obavijest",
                column: "KategorijaObavijestiId");

            migrationBuilder.CreateIndex(
                name: "IX_Obavijest_KorisnickiRacunId",
                table: "Obavijest",
                column: "KorisnickiRacunId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Obavijest");

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Kurs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }
    }
}
