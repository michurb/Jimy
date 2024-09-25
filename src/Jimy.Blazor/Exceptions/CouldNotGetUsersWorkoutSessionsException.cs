namespace Jimy.Blazor.Exceptions;

public sealed class CouldNotGetUsersWorkoutSessionsException : CoreException
{
    public CouldNotGetUsersWorkoutSessionsException() 
        : base($"Could not found your's workout sessions.") {}
}