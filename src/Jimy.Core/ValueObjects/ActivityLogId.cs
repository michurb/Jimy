using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record ActivityLogId
{
    public Guid Value { get; }

    public ActivityLogId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidActivityLogIdException();
        Value = value;
    }
    public static ActivityLogId NewId() => new(Guid.NewGuid());
}