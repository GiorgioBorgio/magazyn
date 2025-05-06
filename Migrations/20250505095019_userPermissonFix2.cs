using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magazyn.Migrations
{
    /// <inheritdoc />
    public partial class userPermissonFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserPermissions_UserPermissionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserPermissionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserPermissionId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "UserPermissonsId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserUserPermission",
                columns: table => new
                {
                    UserPermissionsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUserPermission", x => new { x.UserPermissionsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserUserPermission_UserPermissions_UserPermissionsId",
                        column: x => x.UserPermissionsId,
                        principalTable: "UserPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUserPermission_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserUserPermission_UsersId",
                table: "UserUserPermission",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUserPermission");

            migrationBuilder.DropColumn(
                name: "UserPermissonsId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserPermissionId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPermissionId",
                table: "Users",
                column: "UserPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserPermissions_UserPermissionId",
                table: "Users",
                column: "UserPermissionId",
                principalTable: "UserPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
