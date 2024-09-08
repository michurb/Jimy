namespace Jimy.Business.DTOs;

public class CreateWorkoutSessionDto
{
    public Guid UserId { get; set; }
    public int WorkoutPlanId { get; set; }
    public List<CreateWorkoutSessionExerciseDto> Exercises { get; set; }
}