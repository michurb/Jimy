namespace Jimy.Core.Entities;

public class WorkoutPlan
{
    public int Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public ICollection<WorkoutExercise> Exercises { get; private set; }

    protected WorkoutPlan() {}

    public WorkoutPlan(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
        CreatedDate = DateTime.UtcNow;
        Exercises = new List<WorkoutExercise>();
    }

    public void AddExercise(Exercise exercise, int sets, int reps)
    {
        Exercises.Add(new WorkoutExercise(this, exercise, sets, reps));
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}