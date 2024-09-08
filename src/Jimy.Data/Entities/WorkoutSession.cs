namespace Jimy.Data.Entities;

public class WorkoutSession
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int WorkoutPlanId { get; set; }
    public WorkoutPlan WorkoutPlan { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public List<WorkoutSessionExercise> Exercises { get; set; } = new List<WorkoutSessionExercise>();
}