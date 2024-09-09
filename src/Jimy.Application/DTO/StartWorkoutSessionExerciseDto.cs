namespace Jimy.Application.DTO;

public class StartWorkoutSessionExerciseDto
{
    public int ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public decimal Weight { get; set; }
}