using Jimy.Core.ValueObjects;

namespace Jimy.Core.Entities;

public class WorkoutExercise
{
    public ExerciseId ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; }
    public Sets Sets { get; private set; }
    public Reps Reps { get; private set; }
    
    private WorkoutExercise() {}

    public WorkoutExercise(ExerciseId exerciseId, Sets sets, Reps reps)
    {
        ExerciseId = exerciseId;
        Sets = sets;
        Reps = reps;
    }
}