using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record UserId
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidUserIdException();
        Value = value;
    }

    public static UserId NewId() => new (Guid.NewGuid());
}