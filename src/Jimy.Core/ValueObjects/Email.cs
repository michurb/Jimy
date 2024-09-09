using System.Text.RegularExpressions;
using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidEmailException("Email cannot be empty.");
        if (!Regex.IsMatch(value, @"^(.+)@(.+)$"))
            throw new InvalidEmailException("Invalid email format.");
        Value = value.ToLowerInvariant();
    }
}