namespace Jimy.Core.Exceptions;

public sealed class WorkoutPlanNotFoundException : CoreException
{
    public WorkoutPlanNotFoundException(Guid workoutPlanId) 
        : base($"Workout plan with ID {workoutPlanId} was not found.") {}
}