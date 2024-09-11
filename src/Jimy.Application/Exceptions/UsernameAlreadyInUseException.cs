using Jimy.Core.Exceptions;

namespace Jimy.Application.Exceptions;

public sealed class UsernameAlreadyInUseException : CoreException
{
    public string Username { get; }

    public UsernameAlreadyInUseException(string username) : base($"Username: '{username}' is already in use.")
    {
        Username = username;
    }
}