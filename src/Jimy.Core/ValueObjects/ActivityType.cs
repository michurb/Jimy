using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record ActivityType
{
    public string Value { get; }

    public ActivityType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidActivityTypeException();
        }
        Value = value;
    }
}