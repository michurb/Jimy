namespace Jimy.Blazor.Exceptions;

public sealed class CouldNotStartWorkoutSeesionException : CoreException
{
    public CouldNotStartWorkoutSeesionException() 
        : base($"Could not start workout session.") {}
}