using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record Role
{
    public static IEnumerable<string> AvailableRoles { get; } = new[] {"admin", "user"};
    public string Value { get; }

    public Role(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidRoleException("Role cannot be empty.");
        if (!AvailableRoles.Contains(value))
        {
            throw new InvalidRoleException(value);
        }
        Value = value;
    }

    public static Role User() => new Role("user");
    public static Role Admin() => new Role("admin");
    public static implicit operator Role(string value) => new Role(value);

    public static implicit operator string(Role value) => value?.Value;
    
    public override string ToString() => Value;
}