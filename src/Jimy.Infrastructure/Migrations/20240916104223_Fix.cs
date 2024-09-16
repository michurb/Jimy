using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jimy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercise_WorkoutPlans_WorkoutPlanId",
                table: "WorkoutExercise");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercise_WorkoutPlans_WorkoutPlanId",
                table: "WorkoutExercise",
                column: "WorkoutPlanId",
                principalTable: "WorkoutPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercise_WorkoutPlans_WorkoutPlanId",
                table: "WorkoutExercise");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercise_WorkoutPlans_WorkoutPlanId",
                table: "WorkoutExercise",
                column: "WorkoutPlanId",
                principalTable: "WorkoutPlans",
                principalColumn: "Id");
        }
    }
}
