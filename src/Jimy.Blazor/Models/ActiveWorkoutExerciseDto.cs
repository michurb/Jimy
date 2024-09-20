namespace Jimy.Blazor.Models;

public class ActiveWorkoutExerciseDto
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public int PlannedSets { get; set; }
    public int PlannedReps { get; set; }
    public List<WorkoutSetDto> Sets { get; set; } = new List<WorkoutSetDto>();
}