namespace Jimy.Core.Exceptions;

public sealed class InvalidEmailException : CoreException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email: {email} is invalid")
    {
        Email = email;
    }
}