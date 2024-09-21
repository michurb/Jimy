using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;

namespace Jimy.Core.Interfaces;

public interface IWorkoutSessionRepository
{
    Task<WorkoutSession> GetByIdAsync(WorkoutSessionId id);
    Task<IEnumerable<WorkoutSession>> GetByUserIdAsync(UserId userId);
    Task AddAsync(WorkoutSession workoutSession);
    Task UpdateAsync(WorkoutSession workoutSession);
    Task DeleteAsync(WorkoutSessionId id);
    Task<IEnumerable<WorkoutSession>> GetActiveWorkoutSessionsAsync(UserId userId);
}