namespace Jimy.Blazor.Exceptions;

public sealed class CouldNotFindWorkoutPlanException : CoreException
{
    public CouldNotFindWorkoutPlanException() 
        : base($"Could not find workout plan.") {}
}