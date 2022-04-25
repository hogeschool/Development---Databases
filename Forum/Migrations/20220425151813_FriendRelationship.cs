using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    public partial class FriendRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_Friend",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    FriendId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Friend", x => new { x.UserId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_User_Friend_AuthenticatedUser_FriendId",
                        column: x => x.FriendId,
                        principalTable: "AuthenticatedUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Friend_AuthenticatedUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AuthenticatedUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Friend_FriendId",
                table: "User_Friend",
                column: "FriendId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Friend");
        }
    }
}
