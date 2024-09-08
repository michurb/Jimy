using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Commands;

public class StartWorkoutSession : ICommand
{
    public Guid UserId { get; }
    public int WorkoutPlanId { get; }
    public List<WorkoutSessionExerciseInput> Exercises { get; }
    public int SessionId { get; set; }

    public StartWorkoutSession(Guid userId, int workoutPlanId, List<WorkoutSessionExerciseInput> exercises)
    {
        UserId = userId;
        WorkoutPlanId = workoutPlanId;
        Exercises = exercises;
    }
}