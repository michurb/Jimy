namespace Jimy.Data.Entities;

public class WorkoutExercise
{
    public int Id { get; set; }
    public int WorkoutPlanId { get; set; }
    public int ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
}