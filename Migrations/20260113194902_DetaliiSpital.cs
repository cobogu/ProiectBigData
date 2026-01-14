using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectBigData.Migrations
{
    /// <inheritdoc />
    public partial class DetaliiSpital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NrPaturiOcupate",
                table: "Spitale",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NrPaturiTotal",
                table: "Spitale",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NrPersonal",
                table: "Spitale",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NrPaturiOcupate",
                table: "Spitale");

            migrationBuilder.DropColumn(
                name: "NrPaturiTotal",
                table: "Spitale");

            migrationBuilder.DropColumn(
                name: "NrPersonal",
                table: "Spitale");
        }
    }
}
