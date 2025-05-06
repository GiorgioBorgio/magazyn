using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magazyn.Migrations
{
    /// <inheritdoc />
    public partial class userPermissonFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPermissonsId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPermissonsId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
