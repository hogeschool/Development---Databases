using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Forum.Migrations
{
    public partial class AddUnicityConstraintForEmailAndUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "AuthenticatedUser");

            migrationBuilder.AlterColumn<bool>(
                name: "Banned",
                table: "AuthenticatedUser",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AuthenticatedUser",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthenticatedUser",
                table: "AuthenticatedUser",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticatedUser_Email",
                table: "AuthenticatedUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticatedUser_UserName",
                table: "AuthenticatedUser",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthenticatedUser",
                table: "AuthenticatedUser");

            migrationBuilder.DropIndex(
                name: "IX_AuthenticatedUser_Email",
                table: "AuthenticatedUser");

            migrationBuilder.DropIndex(
                name: "IX_AuthenticatedUser_UserName",
                table: "AuthenticatedUser");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AuthenticatedUser");

            migrationBuilder.RenameTable(
                name: "AuthenticatedUser",
                newName: "User");

            migrationBuilder.AlterColumn<bool>(
                name: "Banned",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    PasswordSalt = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });
        }
    }
}
