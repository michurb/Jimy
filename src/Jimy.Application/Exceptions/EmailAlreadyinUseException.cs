using Jimy.Core.Exceptions;

namespace Jimy.Application.Exceptions;

public sealed class EmailAlreadyInUseException : CoreException
{
    public string Email { get; }

    public EmailAlreadyInUseException(string email) : base($"Email: '{email}' is already in use.")
    {
        Email = email;
    }
}