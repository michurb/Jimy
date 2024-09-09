using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record ExerciseName
{
    public string Value { get; }

    public ExerciseName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidExerciseNameException();
        if (value.Length > 100)
            throw new InvalidExerciseNameException();
        Value = value;
    }
}