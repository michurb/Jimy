using Jimy.Data.Entities;

namespace Jimy.Data.Interfaces;

public interface IWorkoutSessionRepository : IGenericRepository<WorkoutSession>
{
    Task<IEnumerable<WorkoutSession>> GetByUserIdAsync(Guid userId);
    Task<WorkoutSession> GetWithExercisesAsync(int id);
}