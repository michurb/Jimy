using Jimy.Core.Exceptions;
using Jimy.Core.ValueObjects;

namespace Jimy.Core.Entities;

public class WorkoutSession
{
    public WorkoutSessionId Id { get; private set; }
    public UserId UserId { get; private set; }
    public WorkoutPlanId WorkoutPlanId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    private readonly List<WorkoutSessionExercise> _exercises = new();
    public IReadOnlyCollection<WorkoutSessionExercise> Exercises => _exercises.AsReadOnly();
    private const int MaxWorkoutDurationHours = 5;

    private WorkoutSession() { }

    public WorkoutSession(WorkoutSessionId id, UserId userId, WorkoutPlanId workoutPlanId, DateTime startTime,
        IEnumerable<WorkoutSessionExercise> exercises)
    {
        Id = id;
        UserId = userId;
        WorkoutPlanId = workoutPlanId;
        StartTime = startTime;
        _exercises.AddRange(exercises);
    }

    public void AddExercise(ExerciseId exerciseId, Sets sets, Reps reps, Weight weight)
    {
        _exercises.Add(new WorkoutSessionExercise(exerciseId, sets, reps, weight));
    }

    public void End(DateTime endTime)
    {
        if (EndTime.HasValue)
        {
            throw new WorkoutSessionAlreadyEndedException(Id.Value);
        }

        TimeSpan duration = endTime - StartTime;
        if (duration.TotalHours > MaxWorkoutDurationHours)
        {
            throw new WorkoutSessionExceedMaxDurationException(Id.Value, MaxWorkoutDurationHours);
        }

        EndTime = endTime;
    }
}