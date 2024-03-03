using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Data.Migrations
{
    /// <inheritdoc />
    public partial class StartDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IMAGE_EXERCISES",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExerciseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileByte = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMAGE_EXERCISES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUAL_ENTITIES",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUAL_ENTITIES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    EmailConfirmation = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PROFESSIONALS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cref = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndividualEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROFESSIONALS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROFESSIONALS_INDIVIDUAL_ENTITIES_IndividualEntityId",
                        column: x => x.IndividualEntityId,
                        principalTable: "INDIVIDUAL_ENTITIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginConfirmations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<int>(type: "int", maxLength: 6, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginConfirmations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginConfirmations_Logins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Logins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndividualEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "WORKOUTS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Division = table.Column<int>(type: "int", nullable: false),
                    PersonalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndividualEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WORKOUTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WORKOUTS_IMAGE_EXERCISES_ImageExerciseId",
                        column: x => x.ImageExerciseId,
                        principalTable: "IMAGE_EXERCISES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WORKOUTS_INDIVIDUAL_ENTITIES_IndividualEntityId",
                        column: x => x.IndividualEntityId,
                        principalTable: "INDIVIDUAL_ENTITIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WORKOUTS_PROFESSIONALS_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "PROFESSIONALS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EXERCISES",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberSeries = table.Column<int>(type: "int", precision: 2, scale: 0, nullable: false),
                    Repetitions = table.Column<int>(type: "int", precision: 2, scale: 0, nullable: false),
                    RestTime = table.Column<int>(type: "int", precision: 2, scale: 0, nullable: false),
                    ImageExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EXERCISES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EXERCISES_IMAGE_EXERCISES_ImageExerciseId",
                        column: x => x.ImageExerciseId,
                        principalTable: "IMAGE_EXERCISES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EXERCISES_WORKOUTS_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "WORKOUTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EXERCISES_ImageExerciseId",
                table: "EXERCISES",
                column: "ImageExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_EXERCISES_WorkoutId",
                table: "EXERCISES",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_IMAGE_EXERCISES_ExerciseName",
                table: "IMAGE_EXERCISES",
                column: "ExerciseName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUAL_ENTITIES_Cpf",
                table: "INDIVIDUAL_ENTITIES",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginConfirmations_LoginId",
                table: "LoginConfirmations",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_Email",
                table: "Logins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROFESSIONALS_IndividualEntityId",
                table: "PROFESSIONALS",
                column: "IndividualEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_IndividualEntityId",
                table: "USERS",
                column: "IndividualEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_LoginId",
                table: "USERS",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_WORKOUTS_ImageExerciseId",
                table: "WORKOUTS",
                column: "ImageExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WORKOUTS_IndividualEntityId",
                table: "WORKOUTS",
                column: "IndividualEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WORKOUTS_PersonalId",
                table: "WORKOUTS",
                column: "PersonalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EXERCISES");

            migrationBuilder.DropTable(
                name: "LoginConfirmations");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "WORKOUTS");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "IMAGE_EXERCISES");

            migrationBuilder.DropTable(
                name: "PROFESSIONALS");

            migrationBuilder.DropTable(
                name: "INDIVIDUAL_ENTITIES");
        }
    }
}
