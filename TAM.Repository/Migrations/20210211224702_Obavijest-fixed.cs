using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Obavijestfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Obavijest_AspNetUsers_KorisnickiRacunId",
                table: "Obavijest");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnickiRacunId",
                table: "Obavijest",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Obavijest_AspNetUsers_KorisnickiRacunId",
                table: "Obavijest",
                column: "KorisnickiRacunId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Obavijest_AspNetUsers_KorisnickiRacunId",
                table: "Obavijest");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnickiRacunId",
                table: "Obavijest",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Obavijest_AspNetUsers_KorisnickiRacunId",
                table: "Obavijest",
                column: "KorisnickiRacunId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
