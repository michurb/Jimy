using Jimy.Core.ValueObjects;

namespace Jimy.Core.Entities;

public class WorkoutSessionExercise
{
    public ExerciseId ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; }
    public WorkoutSessionId WorkoutSessionId { get; private set; }    
    public Sets Sets { get; private set; }
    public Reps Reps { get; private set; }
    public Weight Weight { get; private set; }

    public WorkoutSessionExercise(ExerciseId exerciseId, Sets sets, Reps reps, Weight weight)
    {
        ExerciseId = exerciseId;
        Sets = sets;
        Reps = reps;
        Weight = weight;
    }

    public void Update(Sets sets, Reps reps, Weight weight)
    {
        Sets = sets;
        Reps = reps;
        Weight = weight;
    }
}