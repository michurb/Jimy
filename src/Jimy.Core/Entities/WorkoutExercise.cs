namespace Jimy.Core.Entities;

public class WorkoutExercise
{
    public int Id { get; private set; }
    public int WorkoutPlanId { get; private set; }
    public WorkoutPlan WorkoutPlan { get; private set; }
    public int ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; }
    public int Sets { get; private set; }
    public int Reps { get; private set; }

    protected WorkoutExercise() {}

    public WorkoutExercise(WorkoutPlan workoutPlan, Exercise exercise, int sets, int reps)
    {
        WorkoutPlan = workoutPlan;
        Exercise = exercise;
        Sets = sets;
        Reps = reps;
    }

    public void UpdateSetRep(int sets, int reps)
    {
        Sets = sets;
        Reps = reps;
    }
}