using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class KursPotrebnoOrganizovatiadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PotrebnoOrganizovati",
                table: "Kurs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PotrebnoOrganizovati",
                table: "Kurs");
        }
    }
}
