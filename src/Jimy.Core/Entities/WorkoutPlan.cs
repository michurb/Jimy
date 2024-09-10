using Jimy.Core.ValueObjects;

namespace Jimy.Core.Entities;

public class WorkoutPlan
{
    public WorkoutPlanId Id { get; private set; }
    public UserId UserId { get; private set; }
    public WorkoutPlanName Name { get; private set; }
    public DateTime CreatedDate { get; private set; }
    private readonly List<WorkoutExercise> _exercises = new();
    public IReadOnlyCollection<WorkoutExercise> Exercises => _exercises.AsReadOnly();

    private WorkoutPlan() {}

    public WorkoutPlan(WorkoutPlanId id, UserId userId, WorkoutPlanName name, DateTime createdDate)
    {
        Id = id;
        UserId = userId;
        Name = name;
        CreatedDate = createdDate;
    }

    public void AddExercise(ExerciseId exerciseId, Sets sets, Reps reps)
    {
        _exercises.Add(new WorkoutExercise(exerciseId, sets, reps));
    }

    public void RemoveExercise(ExerciseId exerciseId)
    {
        _exercises.RemoveAll(e => e.ExerciseId == exerciseId);
    }
    
    public void ClearExercises()
    {
        _exercises.Clear();
    }

    public void UpdateName(WorkoutPlanName name)
    {
        Name = name;
    }
}