using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPasswordException();
        if (value.Length < 6)
            throw new InvalidPasswordException();
        Value = value;
    }
}