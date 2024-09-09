namespace Jimy.Application.DTO;

public class UpdateWorkoutExerciseDto
{
    public int? Id { get; set; }
    public int ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
}