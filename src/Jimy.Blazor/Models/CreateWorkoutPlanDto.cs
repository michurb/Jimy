namespace Jimy.Blazor.Models;


public class CreateWorkoutPlanDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public List<WorkoutExerciseDto> Exercises { get; set; } = new List<WorkoutExerciseDto>();
}