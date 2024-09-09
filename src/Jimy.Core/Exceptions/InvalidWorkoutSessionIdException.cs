namespace Jimy.Core.Exceptions;

public sealed class InvalidWorkoutSessionIdException : CoreException
{
    public InvalidWorkoutSessionIdException() : base("Invalid workout session ID.") { }
}