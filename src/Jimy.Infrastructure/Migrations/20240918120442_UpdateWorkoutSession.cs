using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jimy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkoutSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "WorkoutSessionExercises");

            migrationBuilder.CreateTable(
                name: "WorkoutSets",
                columns: table => new
                {
                    SetNumber = table.Column<int>(type: "int", nullable: false),
                    WorkoutSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSets", x => new { x.WorkoutSessionId, x.ExerciseId, x.SetNumber });
                    table.ForeignKey(
                        name: "FK_WorkoutSets_WorkoutSessionExercises_WorkoutSessionId_ExerciseId",
                        columns: x => new { x.WorkoutSessionId, x.ExerciseId },
                        principalTable: "WorkoutSessionExercises",
                        principalColumns: new[] { "WorkoutSessionId", "ExerciseId" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutSets");

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "WorkoutSessionExercises",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
