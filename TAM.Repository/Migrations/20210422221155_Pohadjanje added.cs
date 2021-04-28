using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Pohadjanjeadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pohadjanje",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OcjenaPohadjanja = table.Column<int>(nullable: true),
                    Pohadja = table.Column<bool>(nullable: false),
                    PolaznikId = table.Column<string>(nullable: true),
                    OrganizacijaKursaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pohadjanje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pohadjanje_OrganizacijaKursa_OrganizacijaKursaId",
                        column: x => x.OrganizacijaKursaId,
                        principalTable: "OrganizacijaKursa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Pohadjanje_Polaznik_PolaznikId",
                        column: x => x.PolaznikId,
                        principalTable: "Polaznik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pohadjanje_OrganizacijaKursaId",
                table: "Pohadjanje",
                column: "OrganizacijaKursaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pohadjanje_PolaznikId",
                table: "Pohadjanje",
                column: "PolaznikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pohadjanje");
        }
    }
}
