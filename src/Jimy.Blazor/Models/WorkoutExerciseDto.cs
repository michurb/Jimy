namespace Jimy.Blazor.Models;

public class WorkoutExerciseDto
{
    public Guid WorkoutExerciseId { get; set; }
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
}