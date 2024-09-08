namespace Jimy.Business.Exceptions;

public class WorkoutSessionNotFoundException : CustomException
{
    public WorkoutSessionNotFoundException(int workoutSessionId) : base($"Exercise with ID: '{workoutSessionId}' was not found.")
    {
        WorkoutSessionId = workoutSessionId;
    }

    public int WorkoutSessionId { get; }
}