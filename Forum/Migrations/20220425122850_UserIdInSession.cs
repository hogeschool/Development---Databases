using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    public partial class UserIdInSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_AuthenticatedUser_UserId",
                table: "Session");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Session",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_AuthenticatedUser_UserId",
                table: "Session",
                column: "UserId",
                principalTable: "AuthenticatedUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_AuthenticatedUser_UserId",
                table: "Session");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Session",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_AuthenticatedUser_UserId",
                table: "Session",
                column: "UserId",
                principalTable: "AuthenticatedUser",
                principalColumn: "Id");
        }
    }
}
