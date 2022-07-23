using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hannahM.Migrations
{
    /// <inheritdoc />
    public partial class AlterRandomAndBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RViews",
                table: "Random",
                newName: "Views");

            migrationBuilder.RenameColumn(
                name: "RTitle",
                table: "Random",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "RStatus",
                table: "Random",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "RCreatedDateTime",
                table: "Random",
                newName: "CreatedDateTime");

            migrationBuilder.RenameColumn(
                name: "RContent",
                table: "Random",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "BViews",
                table: "Blog",
                newName: "Views");

            migrationBuilder.RenameColumn(
                name: "BTitle",
                table: "Blog",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "BStatus",
                table: "Blog",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "BCreatedDateTime",
                table: "Blog",
                newName: "CreatedDateTime");

            migrationBuilder.RenameColumn(
                name: "BContent",
                table: "Blog",
                newName: "Content");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Views",
                table: "Random",
                newName: "RViews");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Random",
                newName: "RTitle");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Random",
                newName: "RStatus");

            migrationBuilder.RenameColumn(
                name: "CreatedDateTime",
                table: "Random",
                newName: "RCreatedDateTime");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Random",
                newName: "RContent");

            migrationBuilder.RenameColumn(
                name: "Views",
                table: "Blog",
                newName: "BViews");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Blog",
                newName: "BTitle");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Blog",
                newName: "BStatus");

            migrationBuilder.RenameColumn(
                name: "CreatedDateTime",
                table: "Blog",
                newName: "BCreatedDateTime");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Blog",
                newName: "BContent");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
