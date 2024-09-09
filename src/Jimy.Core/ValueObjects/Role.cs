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

    public static Role User() => new Role("User");
    public static Role Admin() => new Role("Admin");
}