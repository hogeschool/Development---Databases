using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF__Console.Migrations
{
    public partial class AddUserInGroupForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsersInGroup_GroupId",
                table: "UsersInGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInGroup_UserId",
                table: "UsersInGroup",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInGroup_Groups_GroupId",
                table: "UsersInGroup",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInGroup_Users_UserId",
                table: "UsersInGroup",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersInGroup_Groups_GroupId",
                table: "UsersInGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInGroup_Users_UserId",
                table: "UsersInGroup");

            migrationBuilder.DropIndex(
                name: "IX_UsersInGroup_GroupId",
                table: "UsersInGroup");

            migrationBuilder.DropIndex(
                name: "IX_UsersInGroup_UserId",
                table: "UsersInGroup");
        }
    }
}
