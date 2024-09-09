using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record Reps
{
    public int Value { get; }

    public Reps(int value)
    {
        if (value <= 0)
            throw new InvalidRepsException(value);
        Value = value;
    }
}