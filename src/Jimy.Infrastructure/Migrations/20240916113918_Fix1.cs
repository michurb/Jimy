using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jimy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSessionExercise_Exercises_ExerciseId",
                table: "WorkoutSessionExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSessionExercise_WorkoutSessions_WorkoutSessionId",
                table: "WorkoutSessionExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSessionExercise",
                table: "WorkoutSessionExercise");

            migrationBuilder.RenameTable(
                name: "WorkoutSessionExercise",
                newName: "WorkoutSessionExercises");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutSessionExercise_ExerciseId",
                table: "WorkoutSessionExercises",
                newName: "IX_WorkoutSessionExercises_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSessionExercises",
                table: "WorkoutSessionExercises",
                columns: new[] { "WorkoutSessionId", "ExerciseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSessionExercises_Exercises_ExerciseId",
                table: "WorkoutSessionExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSessionExercises_WorkoutSessions_WorkoutSessionId",
                table: "WorkoutSessionExercises",
                column: "WorkoutSessionId",
                principalTable: "WorkoutSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSessionExercises_Exercises_ExerciseId",
                table: "WorkoutSessionExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSessionExercises_WorkoutSessions_WorkoutSessionId",
                table: "WorkoutSessionExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSessionExercises",
                table: "WorkoutSessionExercises");

            migrationBuilder.RenameTable(
                name: "WorkoutSessionExercises",
                newName: "WorkoutSessionExercise");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutSessionExercises_ExerciseId",
                table: "WorkoutSessionExercise",
                newName: "IX_WorkoutSessionExercise_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSessionExercise",
                table: "WorkoutSessionExercise",
                columns: new[] { "WorkoutSessionId", "ExerciseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSessionExercise_Exercises_ExerciseId",
                table: "WorkoutSessionExercise",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSessionExercise_WorkoutSessions_WorkoutSessionId",
                table: "WorkoutSessionExercise",
                column: "WorkoutSessionId",
                principalTable: "WorkoutSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
