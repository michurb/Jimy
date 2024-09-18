namespace Jimy.Blazor.Models;

public class WorkoutSessionDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkoutPlanId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public List<WorkoutSessionExerciseDto> Exercises { get; set; }
}