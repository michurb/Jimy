using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record WorkoutSessionId
{
    public Guid Value { get; }

    public WorkoutSessionId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidWorkoutSessionIdException();
        Value = value;
    }
    public static WorkoutSessionId NewId() => new(Guid.NewGuid());
}