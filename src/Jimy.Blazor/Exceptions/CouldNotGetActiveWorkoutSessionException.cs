namespace Jimy.Blazor.Exceptions;

public sealed class CouldNotGetActiveWorkoutSessionException : CoreException
{
    public CouldNotGetActiveWorkoutSessionException() 
        : base($"Could not active workout session.") {}
}