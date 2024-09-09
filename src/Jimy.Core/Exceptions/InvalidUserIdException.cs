namespace Jimy.Core.Exceptions;

public sealed class InvalidUserIdException : CoreException
{
    public InvalidUserIdException() : base("Invalid user ID.") { }
}