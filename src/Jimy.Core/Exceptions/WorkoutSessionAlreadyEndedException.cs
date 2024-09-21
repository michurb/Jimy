namespace Jimy.Core.Exceptions;

public sealed class WorkoutSessionAlreadyEndedException : CoreException
{
    public WorkoutSessionAlreadyEndedException(Guid sessionId) 
        : base($"Workout session {sessionId} has already been ended.")
    {
    }
}