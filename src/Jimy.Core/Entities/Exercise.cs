using Jimy.Core.ValueObjects;

namespace Jimy.Core.Entities;

public class Exercise
{
    public ExerciseId Id { get; private set; }
    public ExerciseName Name { get; private set; }
    public ExerciseDescription Description { get; private set; }

    private Exercise() {}

    public Exercise(ExerciseId id, ExerciseName name, ExerciseDescription description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public void UpdateDetails(ExerciseName name, ExerciseDescription description)
    {
        Name = name;
        Description = description;
    }
}