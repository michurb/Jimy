namespace Jimy.Blazor.Exceptions;

public sealed class FailedToDeleteWorkoutPlanException : CoreException
{
    public FailedToDeleteWorkoutPlanException() 
        : base($"Failed to delete workout plan.") {}
}