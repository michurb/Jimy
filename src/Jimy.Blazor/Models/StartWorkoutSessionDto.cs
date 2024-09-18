namespace Jimy.Blazor.Models;

public class StartWorkoutSessionDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkoutPlanId { get; set; }
    public List<WorkoutSessionExerciseDto> Exercises { get; set; } = new();
}

public class ActiveWorkoutSessionDto
{
    public Guid Id { get; set; }
    public Guid WorkoutPlanId { get; set; }
    public string WorkoutPlanName { get; set; }
    public DateTime StartTime { get; set; }
    public List<ActiveWorkoutExerciseDto> Exercises { get; set; } = new List<ActiveWorkoutExerciseDto>();
}

public class ActiveWorkoutExerciseDto
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public int PlannedSets { get; set; }
    public int PlannedReps { get; set; }
    public List<WorkoutSetDto> Sets { get; set; } = new List<WorkoutSetDto>();
}

public class WorkoutSetDto
{
    public int SetNumber { get; set; }
    public int Reps { get; set; }
    public decimal Weight { get; set; }
}