namespace Jimy.Blazor.Models;

public class WorkoutSessionDto
{
    public Guid Id { get; set; }
    public string PlanName { get; set; }
    public DateTime StartTime { get; set; }
    public List<WorkoutSessionExerciseDto> Exercises { get; set; }
}