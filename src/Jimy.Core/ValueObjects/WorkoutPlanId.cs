using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record WorkoutPlanId
{
    public Guid Value { get; }

    public WorkoutPlanId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidWorkoutPlanIdException();
        Value = value;
    }
    public static WorkoutPlanId NewId() => new(Guid.NewGuid());
}