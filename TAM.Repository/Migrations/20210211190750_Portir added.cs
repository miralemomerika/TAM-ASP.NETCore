using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Portiradded : Migration
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
                name: "Portir",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portir", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portir_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Portir");

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
