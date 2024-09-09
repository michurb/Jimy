using Jimy.Core.Entities;

namespace Jimy.Core.Interfaces;

public interface IWorkoutPlanRepository
{
    Task<WorkoutPlan> GetByIdAsync(int id);
    Task<IEnumerable<WorkoutPlan>> GetByUserIdAsync(Guid userId);
    Task AddAsync(WorkoutPlan workoutPlan);
    Task UpdateAsync(WorkoutPlan workoutPlan);
    Task DeleteAsync(int id);
}