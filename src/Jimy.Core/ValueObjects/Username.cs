using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record Username
{
    public string Value { get; }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidUsernameException("Username cannot be empty.");
        if (value.Length < 3 || value.Length > 30)
            throw new InvalidUsernameException("Username must be between 3 and 30 characters.");
        Value = value;
    }
    
    public static implicit operator Username(string value) => new Username(value);

    public static implicit operator string(Username value) => value?.Value;

    public override string ToString() => Value;
}