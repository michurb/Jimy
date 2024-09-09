using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record FullName
{
    public string Value { get; }

    public FullName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidFullNameException("Full name cannot be empty.");
        if (value.Length > 100)
            throw new InvalidFullNameException("Full name cannot exceed 100 characters.");
        Value = value;
    }
}