using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EXERCISES_IMAGE_EXERCISES_ImageExerciseId",
                table: "EXERCISES");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualEntity_Logins_LoginId",
                table: "IndividualEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PROFESSIONALS_IndividualEntity_IndividualEntityId",
                table: "PROFESSIONALS");

            migrationBuilder.DropForeignKey(
                name: "FK_WORKOUTS_IMAGE_EXERCISES_ImageExerciseId",
                table: "WORKOUTS");

            migrationBuilder.DropForeignKey(
                name: "FK_WORKOUTS_IndividualEntity_FitnessClientId",
                table: "WORKOUTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualEntity",
                table: "IndividualEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IMAGE_EXERCISES",
                table: "IMAGE_EXERCISES");

            migrationBuilder.RenameTable(
                name: "IndividualEntity",
                newName: "IndividualEntities");

            migrationBuilder.RenameTable(
                name: "IMAGE_EXERCISES",
                newName: "ImageExercises");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualEntity_LoginId",
                table: "IndividualEntities",
                newName: "IX_IndividualEntities_LoginId");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualEntity_Cpf",
                table: "IndividualEntities",
                newName: "IX_IndividualEntities_Cpf");

            migrationBuilder.RenameIndex(
                name: "IX_IMAGE_EXERCISES_ExerciseName",
                table: "ImageExercises",
                newName: "IX_ImageExercises_ExerciseName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualEntities",
                table: "IndividualEntities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageExercises",
                table: "ImageExercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EXERCISES_ImageExercises_ImageExerciseId",
                table: "EXERCISES",
                column: "ImageExerciseId",
                principalTable: "ImageExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualEntities_Logins_LoginId",
                table: "IndividualEntities",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PROFESSIONALS_IndividualEntities_IndividualEntityId",
                table: "PROFESSIONALS",
                column: "IndividualEntityId",
                principalTable: "IndividualEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WORKOUTS_ImageExercises_ImageExerciseId",
                table: "WORKOUTS",
                column: "ImageExerciseId",
                principalTable: "ImageExercises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WORKOUTS_IndividualEntities_FitnessClientId",
                table: "WORKOUTS",
                column: "FitnessClientId",
                principalTable: "IndividualEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EXERCISES_ImageExercises_ImageExerciseId",
                table: "EXERCISES");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualEntities_Logins_LoginId",
                table: "IndividualEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_PROFESSIONALS_IndividualEntities_IndividualEntityId",
                table: "PROFESSIONALS");

            migrationBuilder.DropForeignKey(
                name: "FK_WORKOUTS_ImageExercises_ImageExerciseId",
                table: "WORKOUTS");

            migrationBuilder.DropForeignKey(
                name: "FK_WORKOUTS_IndividualEntities_FitnessClientId",
                table: "WORKOUTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndividualEntities",
                table: "IndividualEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageExercises",
                table: "ImageExercises");

            migrationBuilder.RenameTable(
                name: "IndividualEntities",
                newName: "IndividualEntity");

            migrationBuilder.RenameTable(
                name: "ImageExercises",
                newName: "IMAGE_EXERCISES");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualEntities_LoginId",
                table: "IndividualEntity",
                newName: "IX_IndividualEntity_LoginId");

            migrationBuilder.RenameIndex(
                name: "IX_IndividualEntities_Cpf",
                table: "IndividualEntity",
                newName: "IX_IndividualEntity_Cpf");

            migrationBuilder.RenameIndex(
                name: "IX_ImageExercises_ExerciseName",
                table: "IMAGE_EXERCISES",
                newName: "IX_IMAGE_EXERCISES_ExerciseName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndividualEntity",
                table: "IndividualEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IMAGE_EXERCISES",
                table: "IMAGE_EXERCISES",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EXERCISES_IMAGE_EXERCISES_ImageExerciseId",
                table: "EXERCISES",
                column: "ImageExerciseId",
                principalTable: "IMAGE_EXERCISES",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_WORKOUTS_IMAGE_EXERCISES_ImageExerciseId",
                table: "WORKOUTS",
                column: "ImageExerciseId",
                principalTable: "IMAGE_EXERCISES",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WORKOUTS_IndividualEntity_FitnessClientId",
                table: "WORKOUTS",
                column: "FitnessClientId",
                principalTable: "IndividualEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
