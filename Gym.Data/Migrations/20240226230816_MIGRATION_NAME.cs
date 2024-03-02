using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Data.Migrations
{
    /// <inheritdoc />
    public partial class MIGRATION_NAME : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_USERS_Email",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "USERS");

            migrationBuilder.AddColumn<Guid>(
                name: "LoginId",
                table: "USERS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USERS_LoginId",
                table: "USERS",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_IMAGE_EXERCISES_ExerciseName",
                table: "IMAGE_EXERCISES",
                column: "ExerciseName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_USERS_Login_LoginId",
                table: "USERS",
                column: "LoginId",
                principalTable: "Login",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USERS_Login_LoginId",
                table: "USERS");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropIndex(
                name: "IX_USERS_LoginId",
                table: "USERS");

            migrationBuilder.DropIndex(
                name: "IX_IMAGE_EXERCISES_ExerciseName",
                table: "IMAGE_EXERCISES");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "USERS");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "USERS",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "USERS",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_Email",
                table: "USERS",
                column: "Email",
                unique: true);
        }
    }
}
