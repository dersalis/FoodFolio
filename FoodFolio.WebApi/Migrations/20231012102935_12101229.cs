using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodFolio.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class _12101229 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ServingDate",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServingDate",
                table: "Dishes");
        }
    }
}
