using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class OrganizacijaaktivnaRecenzijaadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aktivna",
                table: "Recenzija");

            migrationBuilder.AddColumn<bool>(
                name: "AktivnaRecenzija",
                table: "OrganizacijaKursa",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AktivnaRecenzija",
                table: "OrganizacijaKursa");

            migrationBuilder.AddColumn<bool>(
                name: "Aktivna",
                table: "Recenzija",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
