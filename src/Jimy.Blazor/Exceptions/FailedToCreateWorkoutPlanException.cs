namespace Jimy.Blazor.Exceptions;

public sealed class FailedToCreateWorkoutPlanException : CoreException
{
    public FailedToCreateWorkoutPlanException() 
        : base($"Failed to create workout plan.") {}
}