using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameWorkou : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INDIVIDUAL_ENTITIES_Logins_LoginId",
                table: "INDIVIDUAL_ENTITIES");

            migrationBuilder.DropForeignKey(
                name: "FK_PROFESSIONALS_INDIVIDUAL_ENTITIES_IndividualEntityId",
                table: "PROFESSIONALS");

            migrationBuilder.DropForeignKey(
                name: "FK_WORKOUTS_INDIVIDUAL_ENTITIES_IndividualEntityId",
                table: "WORKOUTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_INDIVIDUAL_ENTITIES",
                table: "INDIVIDUAL_ENTITIES");

            migrationBuilder.RenameTable(
                name: "INDIVIDUAL_ENTITIES",
                newName: "IndividualEntity");

            migrationBuilder.RenameColumn(
                name: "IndividualEntityId",
                table: "WORKOUTS",
                newName: "FitnessClientId");

            migrationBuilder.RenameIndex(
                name: "IX_WORKOUTS_IndividualEntityId",
                table: "WORKOUTS",
                newName: "IX_WORKOUTS_FitnessClientId");

            migrationBuilder.RenameIndex(
                name: "IX_INDIVIDUAL_ENTITIES_LoginId",
                table: "IndividualEntity",
                newName: "IX_IndividualEntity_LoginId");

            migrationBuilder.RenameIndex(
                name: "IX_INDIVIDUAL_ENTITIES_Cpf",
                table: "IndividualEntity",
                newName: "IX_IndividualEntity_Cpf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualEntity",
                table: "IndividualEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualEntity_Logins_LoginId",
                table: "IndividualEntity",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PROFESSIONALS_IndividualEntity_IndividualEntityId",
                table: "PROFESSIONALS",
                column: "IndividualEntityId",
                principalTable: "IndividualEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WORKOUTS_IndividualEntity_FitnessClientId",
                table: "WORKOUTS",
                column: "FitnessClientId",
                principalTable: "IndividualEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualEntity_Logins_LoginId",
                table: "IndividualEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PROFESSIONALS_IndividualEntity_IndividualEntityId",
                table: "PROFESSIONALS");

            migrationBuilder.DropForeignKey(
                name: "FK_WORKOUTS_IndividualEntity_FitnessClientId",
                table: "WORKOUTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualEntity",
                table: "IndividualEntity");

            migrationBuilder.RenameTable(
                name: "IndividualEntity",
                newName: "INDIVIDUAL_ENTITIES");

            migrationBuilder.RenameColumn(
                name: "FitnessClientId",
                table: "WORKOUTS",
                newName: "IndividualEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_WORKOUTS_FitnessClientId",
                table: "WORKOUTS",
                newName: "IX_WORKOUTS_IndividualEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualEntity_LoginId",
                table: "INDIVIDUAL_ENTITIES",
                newName: "IX_INDIVIDUAL_ENTITIES_LoginId");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualEntity_Cpf",
                table: "INDIVIDUAL_ENTITIES",
                newName: "IX_INDIVIDUAL_ENTITIES_Cpf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_INDIVIDUAL_ENTITIES",
                table: "INDIVIDUAL_ENTITIES",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_INDIVIDUAL_ENTITIES_Logins_LoginId",
                table: "INDIVIDUAL_ENTITIES",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PROFESSIONALS_INDIVIDUAL_ENTITIES_IndividualEntityId",
                table: "PROFESSIONALS",
                column: "IndividualEntityId",
                principalTable: "INDIVIDUAL_ENTITIES",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WORKOUTS_INDIVIDUAL_ENTITIES_IndividualEntityId",
                table: "WORKOUTS",
                column: "IndividualEntityId",
                principalTable: "INDIVIDUAL_ENTITIES",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
