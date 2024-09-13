using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jimy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToWorkoutExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutExercise",
                table: "WorkoutExercise");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "WorkoutExercise",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutExercise",
                table: "WorkoutExercise",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercise_ExerciseId",
                table: "WorkoutExercise",
                column: "ExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutExercise",
                table: "WorkoutExercise");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutExercise_ExerciseId",
                table: "WorkoutExercise");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WorkoutExercise");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutExercise",
                table: "WorkoutExercise",
                column: "ExerciseId");
        }
    }
}
