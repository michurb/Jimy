using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record WorkoutPlanName
{
    public string Value { get; }

    public WorkoutPlanName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidWorkoutPlanNameException("Workout plan name cannot be empty.");
        if (value.Length > 100)
            throw new InvalidWorkoutPlanNameException("Workout plan name cannot exceed 100 characters.");
        Value = value;
    }
}