using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Kurskapacitetadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Kapacitet",
                table: "Kurs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kapacitet",
                table: "Kurs");
        }
    }
}
