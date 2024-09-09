namespace Jimy.Application.DTO;

public class WorkoutExerciseDto
{
    public int Id { get; set; }
    public int ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
}