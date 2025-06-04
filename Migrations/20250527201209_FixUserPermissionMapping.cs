using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magazyn.Migrations
{
    /// <inheritdoc />
    public partial class FixUserPermissionMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUserPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions");

            migrationBuilder.DropColumn(
    name: "Id",
    table: "UserPermissions");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserPermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);


            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions",
                columns: new[] { "UserId", "PermissionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                table: "UserPermissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserPermissions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions",
                column: "Id");

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
    }
}
