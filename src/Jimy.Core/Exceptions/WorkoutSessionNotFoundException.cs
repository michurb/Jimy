namespace Jimy.Core.Exceptions;

public sealed class WorkoutSessionNotFoundException : CoreException
{
    public WorkoutSessionNotFoundException (Guid workoutSessionId) 
        : base($"WorkoutSession with ID {workoutSessionId} was not found.") {}
}