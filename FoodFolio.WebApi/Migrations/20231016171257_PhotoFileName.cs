using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodFolio.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class PhotoFileName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Dishes",
                newName: "PhotoFileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoFileName",
                table: "Dishes",
                newName: "PhotoUrl");
        }
    }
}
