using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record ExerciseId
{
    public int Value { get; }

    public ExerciseId(int value)
    {
        if (value <= 0)
            throw new InvalidExerciseIdException();
        Value = value;
    }
}