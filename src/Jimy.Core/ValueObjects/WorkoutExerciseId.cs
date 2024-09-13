using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record WorkoutExerciseId
{
    public Guid Value { get; }

    public WorkoutExerciseId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidExerciseIdException();
        Value = value;
    }
    public static WorkoutExerciseId NewId() => new(Guid.NewGuid());
}