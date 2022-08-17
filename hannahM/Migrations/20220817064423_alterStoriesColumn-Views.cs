using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hannahM.Migrations
{
    /// <inheritdoc />
    public partial class alterStoriesColumnViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "temp",
                table: "Stories");

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Stories",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "Stories");

            migrationBuilder.AddColumn<string>(
                name: "temp",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
