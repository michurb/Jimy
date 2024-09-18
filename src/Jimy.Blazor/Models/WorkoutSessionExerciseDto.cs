namespace Jimy.Blazor.Models;

public class WorkoutSessionExerciseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<SetDto> Sets { get; set; }
}