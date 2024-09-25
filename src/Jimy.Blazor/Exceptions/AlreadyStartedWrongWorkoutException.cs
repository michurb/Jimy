namespace Jimy.Blazor.Exceptions;

public sealed class AlreadyStartedWrongWorkoutException : CoreException
{
    public AlreadyStartedWrongWorkoutException() 
        : base($"You have active workout session.") {}
}