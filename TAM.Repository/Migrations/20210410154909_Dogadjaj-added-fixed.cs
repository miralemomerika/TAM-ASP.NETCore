using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Dogadjajaddedfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizatorId",
                table: "Dogadjaj",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dogadjaj_OrganizatorId",
                table: "Dogadjaj",
                column: "OrganizatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogadjaj_Organizator_OrganizatorId",
                table: "Dogadjaj",
                column: "OrganizatorId",
                principalTable: "Organizator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogadjaj_Organizator_OrganizatorId",
                table: "Dogadjaj");

            migrationBuilder.DropIndex(
                name: "IX_Dogadjaj_OrganizatorId",
                table: "Dogadjaj");

            migrationBuilder.DropColumn(
                name: "OrganizatorId",
                table: "Dogadjaj");
        }
    }
}
