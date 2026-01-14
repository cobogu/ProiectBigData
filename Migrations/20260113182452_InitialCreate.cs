using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectBigData.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Specializari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spitale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spitale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Varsta = table.Column<int>(type: "int", nullable: false),
                    SpecializareId = table.Column<int>(type: "int", nullable: false),
                    SpitalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctori", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctori_Specializari_SpecializareId",
                        column: x => x.SpecializareId,
                        principalTable: "Specializari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctori_Spitale_SpitalId",
                        column: x => x.SpitalId,
                        principalTable: "Spitale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctori_SpecializareId",
                table: "Doctori",
                column: "SpecializareId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctori_SpitalId",
                table: "Doctori",
                column: "SpitalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctori");

            migrationBuilder.DropTable(
                name: "Specializari");

            migrationBuilder.DropTable(
                name: "Spitale");
        }
    }
}
