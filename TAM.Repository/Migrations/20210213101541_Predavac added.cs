using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Predavacadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Predavac",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Titula = table.Column<string>(nullable: true),
                    CVUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predavac", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Predavac_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Predavac");
        }
    }
}
