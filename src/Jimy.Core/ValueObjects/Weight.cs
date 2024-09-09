using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record Weight
{
    public decimal Value { get; }

    public Weight(decimal value)
    {
        if (value < 0)
            throw new InvalidWeightException();
        Value = value;
    }
}