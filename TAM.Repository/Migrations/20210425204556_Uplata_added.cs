using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TAM.Repository.Migrations
{
    public partial class Uplata_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
             migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionHandler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SadrzajGreske = table.Column<string>(nullable: true),
                    DatumIVrijemeGreske = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionHandler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KategorijaKursa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategorijaKursa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KategorijaObavijesti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategorijaObavijesti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prostorija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 30, nullable: false),
                    BrojMjesta = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prostorija", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SvrhaUplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Svrha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SvrhaUplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipDogadjaja",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipDogadjaja", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipPolaznika",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipPolaznika", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizator",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Institucija = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizator_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Kurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 30, nullable: false),
                    Cijena = table.Column<float>(nullable: false),
                    BrojCasova = table.Column<int>(nullable: false),
                    KategorijaKursaId = table.Column<int>(nullable: false),
                    Kapacitet = table.Column<int>(nullable: false),
                    PotrebnoOrganizovati = table.Column<bool>(nullable: false),
                    Opis = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kurs_KategorijaKursa_KategorijaKursaId",
                        column: x => x.KategorijaKursaId,
                        principalTable: "KategorijaKursa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Obavijest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(maxLength: 100, nullable: false),
                    DatumIVrijeme = table.Column<DateTime>(nullable: false),
                    Sadrzaj = table.Column<string>(nullable: false),
                    KategorijaObavijestiId = table.Column<int>(nullable: false),
                    KorisnickiRacunId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obavijest_KategorijaObavijesti_KategorijaObavijestiId",
                        column: x => x.KategorijaObavijestiId,
                        principalTable: "KategorijaObavijesti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Obavijest_AspNetUsers_KorisnickiRacunId",
                        column: x => x.KorisnickiRacunId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Polaznik",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TipPolaznikaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polaznik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polaznik_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Polaznik_TipPolaznika_TipPolaznikaId",
                        column: x => x.TipPolaznikaId,
                        principalTable: "TipPolaznika",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dogadjaj",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 30, nullable: false),
                    DatumIVrijemeOdrzavanja = table.Column<DateTime>(nullable: false),
                    Odobren = table.Column<bool>(nullable: false),
                    TipDogadjajaId = table.Column<int>(nullable: false),
                    OrganizatorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogadjaj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dogadjaj_Organizator_OrganizatorId",
                        column: x => x.OrganizatorId,
                        principalTable: "Organizator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dogadjaj_TipDogadjaja_TipDogadjajaId",
                        column: x => x.TipDogadjajaId,
                        principalTable: "TipDogadjaja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prijava",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(nullable: false),
                    KursId = table.Column<int>(nullable: false),
                    PolaznikId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prijava", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prijava_Kurs_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prijava_Polaznik_PolaznikId",
                        column: x => x.PolaznikId,
                        principalTable: "Polaznik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });*/

            migrationBuilder.CreateTable(
                name: "Uplata",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(nullable: false),
                    Iznos = table.Column<int>(nullable: false),
                    PrijavaId = table.Column<int>(nullable: true),
                    DogadjajId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uplata_Dogadjaj_DogadjajId",
                        column: x => x.DogadjajId,
                        principalTable: "Dogadjaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uplata_Prijava_PrijavaId",
                        column: x => x.PrijavaId,
                        principalTable: "Prijava",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetRoleClaims_RoleId",
            //    table: "AspNetRoleClaims",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "RoleNameIndex",
            //    table: "AspNetRoles",
            //    column: "NormalizedName",
            //    unique: true,
            //    filter: "[NormalizedName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserClaims_UserId",
            //    table: "AspNetUserClaims",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserLogins_UserId",
            //    table: "AspNetUserLogins",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_RoleId",
            //    table: "AspNetUserRoles",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "EmailIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedEmail");

            //migrationBuilder.CreateIndex(
            //    name: "UserNameIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedUserName",
            //    unique: true,
            //    filter: "[NormalizedUserName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Dogadjaj_OrganizatorId",
            //    table: "Dogadjaj",
            //    column: "OrganizatorId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Dogadjaj_TipDogadjajaId",
            //    table: "Dogadjaj",
            //    column: "TipDogadjajaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Kurs_KategorijaKursaId",
            //    table: "Kurs",
            //    column: "KategorijaKursaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Obavijest_KategorijaObavijestiId",
            //    table: "Obavijest",
            //    column: "KategorijaObavijestiId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Obavijest_KorisnickiRacunId",
            //    table: "Obavijest",
            //    column: "KorisnickiRacunId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Polaznik_TipPolaznikaId",
            //    table: "Polaznik",
            //    column: "TipPolaznikaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Prijava_KursId",
            //    table: "Prijava",
            //    column: "KursId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Prijava_PolaznikId",
            //    table: "Prijava",
            //    column: "PolaznikId");

            migrationBuilder.CreateIndex(
                name: "IX_Uplata_DogadjajId",
                table: "Uplata",
                column: "DogadjajId");

            migrationBuilder.CreateIndex(
                name: "IX_Uplata_PrijavaId",
                table: "Uplata",
                column: "PrijavaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "AspNetRoleClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserLogins");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserRoles");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserTokens");

            //migrationBuilder.DropTable(
            //    name: "ExceptionHandler");

            //migrationBuilder.DropTable(
            //    name: "Obavijest");

            //migrationBuilder.DropTable(
            //    name: "Portir");

            //migrationBuilder.DropTable(
            //    name: "Predavac");

            //migrationBuilder.DropTable(
            //    name: "Prostorija");

            //migrationBuilder.DropTable(
            //    name: "SvrhaUplate");

            migrationBuilder.DropTable(
                name: "Uplata");

            //migrationBuilder.DropTable(
            //    name: "AspNetRoles");

            //migrationBuilder.DropTable(
            //    name: "KategorijaObavijesti");

            //migrationBuilder.DropTable(
            //    name: "Dogadjaj");

            //migrationBuilder.DropTable(
            //    name: "Prijava");

            //migrationBuilder.DropTable(
            //    name: "Organizator");

            //migrationBuilder.DropTable(
            //    name: "TipDogadjaja");

            //migrationBuilder.DropTable(
            //    name: "Kurs");

            //migrationBuilder.DropTable(
            //    name: "Polaznik");

            //migrationBuilder.DropTable(
            //    name: "KategorijaKursa");

            //migrationBuilder.DropTable(
            //    name: "AspNetUsers");

            //migrationBuilder.DropTable(
            //    name: "TipPolaznika");
        }
    }
}
