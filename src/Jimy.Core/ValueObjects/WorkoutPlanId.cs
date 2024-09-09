using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record WorkoutPlanId
{
    public int Value { get; }

    public WorkoutPlanId(int value)
    {
        if (value <= 0)
            throw new InvalidWorkoutPlanIdException();
        Value = value;
    }
}