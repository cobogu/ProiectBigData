using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectBigData.Migrations
{
    /// <inheritdoc />
    public partial class AddStiri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StiriMedicale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titlu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPublicarii = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StiriMedicale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StiriMedicale_Doctori_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StiriMedicale_DoctorId",
                table: "StiriMedicale",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StiriMedicale");
        }
    }
}
