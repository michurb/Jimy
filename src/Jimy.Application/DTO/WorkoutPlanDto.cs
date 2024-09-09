namespace Jimy.Application.DTO;

public class WorkoutPlanDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<WorkoutExerciseDto> Exercises { get; set; }
}