using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record Sets
{
    public int Value { get; }

    public Sets(int value)
    {
        if (value <= 0)
            throw new InvalidSetsException(value);
        Value = value;
    }
}