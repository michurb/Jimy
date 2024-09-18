namespace Jimy.Blazor.Models;

public class WorkoutSessionExerciseDto
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public string Name { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public List<SetDto> SetDetails { get; set; } = new List<SetDto>();
}