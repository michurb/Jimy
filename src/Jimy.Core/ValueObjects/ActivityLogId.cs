using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record ActivityLogId
{
    public int Value { get; }

    public ActivityLogId(int value)
    {
        if (value <= 0)
        {
            throw new InvalidEntityIdException(value);
        }
        Value = value;
    }
}