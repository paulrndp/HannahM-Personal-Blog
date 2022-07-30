using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hannahM.Migrations
{
    /// <inheritdoc />
    public partial class addtempcolumntostorychapter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "temp",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "temp",
                table: "Chapter",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "temp",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "temp",
                table: "Chapter");
        }
    }
}
