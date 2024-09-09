using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record WorkoutSessionId
{
    public int Value { get; }

    public WorkoutSessionId(int value)
    {
        if (value <= 0)
            throw new InvalidWorkoutSessionIdException();
        Value = value;
    }
}