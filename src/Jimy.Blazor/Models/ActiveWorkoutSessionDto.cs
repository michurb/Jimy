namespace Jimy.Blazor.Models;

public class ActiveWorkoutSessionDto
{
    public Guid Id { get; set; }
    public Guid WorkoutPlanId { get; set; }
    public string WorkoutPlanName { get; set; }
    public DateTime StartTime { get; set; }
    public List<ActiveWorkoutExerciseDto> Exercises { get; set; } = new List<ActiveWorkoutExerciseDto>();
}