namespace Jimy.Core.Exceptions;

public sealed class InvalidActivityTypeException : CoreException
{
    public InvalidActivityTypeException() : base("Activity type cannot be empty.")
    {
    }
}