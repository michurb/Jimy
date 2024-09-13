using Jimy.Core.ValueObjects;

namespace Jimy.Core.Entities;

public class WorkoutExercise
{
    public WorkoutExerciseId Id { get; set; }
    public ExerciseId ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; }
    public Sets Sets { get; private set; }
    public Reps Reps { get; private set; }
    
    private WorkoutExercise() {}

    public WorkoutExercise(WorkoutExerciseId id, ExerciseId exerciseId, Sets sets, Reps reps)
    {
        Id = id;
        ExerciseId = exerciseId;
        Sets = sets;
        Reps = reps;
    }
}