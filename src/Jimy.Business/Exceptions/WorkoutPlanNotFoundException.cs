namespace Jimy.Business.Exceptions;

public class WorkoutPlanNotFoundException : CustomException
{
    public WorkoutPlanNotFoundException(int workoutPlanId) : base(
        $"Workout plan with ID: '{workoutPlanId}' was not found.")
    {
        WorkoutPlanId = workoutPlanId;
    }

    public int WorkoutPlanId { get; }
}