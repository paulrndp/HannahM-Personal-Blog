using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hannahM.Migrations
{
    /// <inheritdoc />
    public partial class addColumnStoryChaptersColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalChapters",
                table: "Stories",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalChapters",
                table: "Stories");
        }
    }
}
