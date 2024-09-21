namespace Jimy.Core.Exceptions;

public sealed class WorkoutSessionExceedMaxDurationException : CoreException
{
    public WorkoutSessionExceedMaxDurationException(Guid sessionId, int maxDurationHours) 
        : base($"Workout session {sessionId} has exceeded the maximum duration of {maxDurationHours} hours.")
    {
    }
}