using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record ExerciseDescription
{
    public string Value { get; }

    public ExerciseDescription(string value)
    {
        if (value?.Length > 500)
            throw new InvalidExerciseDescriptionException();
        Value = value ?? string.Empty;
    }
}