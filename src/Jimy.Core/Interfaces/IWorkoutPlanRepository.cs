using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;

namespace Jimy.Core.Interfaces;

public interface IWorkoutPlanRepository
{
    Task<WorkoutPlan> GetByIdAsync(WorkoutPlanId id);
    Task<IEnumerable<WorkoutPlan>> GetByUserIdAsync(UserId userId);
    Task AddAsync(WorkoutPlan workoutPlan);
    Task UpdateAsync(WorkoutPlan workoutPlan);
    Task DeleteAsync(WorkoutPlanId id);
}