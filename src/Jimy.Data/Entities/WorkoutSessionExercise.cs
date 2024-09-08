namespace Jimy.Data.Entities;

public class WorkoutSessionExercise
{
    public int Id { get; set; }
    public int WorkoutSessionId { get; set; }
    public int ExerciseId { get; set; }
    public Exercise Exercise { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public decimal Weight { get; set; }
}