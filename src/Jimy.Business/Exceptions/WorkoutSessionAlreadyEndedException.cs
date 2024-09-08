namespace Jimy.Business.Exceptions;

public class WorkoutSessionAlreadyEndedException : CustomException
{
    public WorkoutSessionAlreadyEndedException(int workoutSessionId) 
        : base($"Workout session with ID: '{workoutSessionId}' has already been ended.")
    {
        WorkoutSessionId = workoutSessionId;
    }

    public int WorkoutSessionId { get; }
}
