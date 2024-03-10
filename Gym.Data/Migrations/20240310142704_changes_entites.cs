using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Data.Migrations
{
    /// <inheritdoc />
    public partial class changes_entites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.AddColumn<Guid>(
                name: "LoginId",
                table: "INDIVIDUAL_ENTITIES",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUAL_ENTITIES_LoginId",
                table: "INDIVIDUAL_ENTITIES",
                column: "LoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_INDIVIDUAL_ENTITIES_Logins_LoginId",
                table: "INDIVIDUAL_ENTITIES",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INDIVIDUAL_ENTITIES_Logins_LoginId",
                table: "INDIVIDUAL_ENTITIES");

            migrationBuilder.DropIndex(
                name: "IX_INDIVIDUAL_ENTITIES_LoginId",
                table: "INDIVIDUAL_ENTITIES");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "INDIVIDUAL_ENTITIES");

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndividualEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USERS_INDIVIDUAL_ENTITIES_IndividualEntityId",
                        column: x => x.IndividualEntityId,
                        principalTable: "INDIVIDUAL_ENTITIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USERS_Logins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Logins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USERS_IndividualEntityId",
                table: "USERS",
                column: "IndividualEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_LoginId",
                table: "USERS",
                column: "LoginId");
        }
    }
}
