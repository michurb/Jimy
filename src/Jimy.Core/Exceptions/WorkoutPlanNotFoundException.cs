namespace Jimy.Core.Exceptions;

public sealed class WorkoutPlanNotFoundException : CoreException
{
    public WorkoutPlanNotFoundException(int workoutPlanId) 
        : base($"Workout plan with ID {workoutPlanId} was not found.") {}
}