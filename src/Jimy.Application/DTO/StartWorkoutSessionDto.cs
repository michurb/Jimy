namespace Jimy.Application.DTO;

public class StartWorkoutSessionDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkoutPlanId { get; set; }
    public List<WorkoutSessionExerciseDto> Exercises { get; set; } = new List<WorkoutSessionExerciseDto>();   
}