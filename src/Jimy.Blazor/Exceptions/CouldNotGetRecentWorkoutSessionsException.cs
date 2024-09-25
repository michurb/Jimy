namespace Jimy.Blazor.Exceptions;

public sealed class CouldNotGetRecentWorkoutSessionsException : CoreException
{
    public CouldNotGetRecentWorkoutSessionsException() 
        : base($"Could not found recent workout session.") {}
}