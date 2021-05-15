using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Recenzijaadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recenzija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OcjenaKursa = table.Column<int>(nullable: false),
                    OcjenaPredavaca = table.Column<int>(nullable: false),
                    Komentar = table.Column<string>(nullable: true),
                    OrganizacijaKursaId = table.Column<int>(nullable: false),
                    Aktivna = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recenzija_OrganizacijaKursa_OrganizacijaKursaId",
                        column: x => x.OrganizacijaKursaId,
                        principalTable: "OrganizacijaKursa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_OrganizacijaKursaId",
                table: "Recenzija",
                column: "OrganizacijaKursaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recenzija");
        }
    }
}
