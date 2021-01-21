using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Kurs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Cijena = table.Column<float>(nullable: false),
                    BrojCasova = table.Column<int>(nullable: false),
                    KategorijaKursaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kurs_KategorijaKursa_KategorijaKursaId",
                        column: x => x.KategorijaKursaId,
                        principalTable: "KategorijaKursa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kurs_KategorijaKursaId",
                table: "Kurs",
                column: "KategorijaKursaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kurs");
        }
    }
}
