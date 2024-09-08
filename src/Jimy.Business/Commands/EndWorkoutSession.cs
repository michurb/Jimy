using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Commands;

public class EndWorkoutSession : ICommand
{
    public int WorkoutSessionId { get; }
    public List<WorkoutSessionExerciseDto> UpdatedExercises { get; }

    public EndWorkoutSession(int workoutSessionId, List<WorkoutSessionExerciseDto> updatedExercises)
    {
        WorkoutSessionId = workoutSessionId;
        UpdatedExercises = updatedExercises;
    }
}