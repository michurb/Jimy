namespace Jimy.Business.DTOs;

public class CreateWorkoutSessionExerciseDto
{
    public int ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public decimal Weight { get; set; }
}