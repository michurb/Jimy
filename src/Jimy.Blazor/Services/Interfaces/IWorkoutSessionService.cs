using Jimy.Blazor.Models;

namespace Jimy.Blazor.API.Interfaces;

public interface IWorkoutSessionService
{
    Task<WorkoutSessionDto> GetSessionAsync(Guid sessionId);
    Task UpdateExerciseWeight(Guid sessionId, Guid exerciseId, int setNumber, decimal weight);
    Task<Guid> StartWorkoutSession(Guid workoutPlanId);
    Task EndWorkoutSessionAsync(Guid sessionId);
    Task<WorkoutSessionDto> GetActiveWorkoutSessionAsync();
}