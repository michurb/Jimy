namespace Jimy.Core.Exceptions;

public sealed class WorkoutSessionAlreadyEndedException : CoreException
{
    public WorkoutSessionAlreadyEndedException (Guid workoutSessionId) 
        : base($"WorkoutSession with ID {workoutSessionId} has been already ended.") {}
}