using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record ExerciseId
{
    public Guid Value { get; }

    public ExerciseId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidExerciseIdException();
        Value = value;
    }
    public static ExerciseId NewId() => new(Guid.NewGuid());
}