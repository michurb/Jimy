namespace Jimy.Core.Exceptions;

public sealed class InvalidPasswordException : CoreException
{
    public InvalidPasswordException() : base("Password is invalid") { }
}