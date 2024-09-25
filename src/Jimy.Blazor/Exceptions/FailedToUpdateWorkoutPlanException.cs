namespace Jimy.Blazor.Exceptions;

public sealed class FailedToUpdateWorkoutPlanException : CoreException
{
    public FailedToUpdateWorkoutPlanException() 
        : base($"Failed to update workout plan.") {}
}