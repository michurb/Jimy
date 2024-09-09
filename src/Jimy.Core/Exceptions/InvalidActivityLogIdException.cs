namespace Jimy.Core.Exceptions;

public sealed class InvalidActivityLogIdException : CoreException
{
    public InvalidActivityLogIdException() : base("Invalid activity log ID.") { }
}
