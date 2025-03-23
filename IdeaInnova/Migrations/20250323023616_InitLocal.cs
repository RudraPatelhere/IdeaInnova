using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeaInnova.Migrations
{
    /// <inheritdoc />
    public partial class InitLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Ideas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_UserId",
                table: "Ideas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Users_UserId",
                table: "Ideas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Users_UserId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_UserId",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Ideas");
        }
    }
}
