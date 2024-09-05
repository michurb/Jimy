using Jimy.Data.Entities;

namespace Jimy.Data.Interfaces;

public interface IWorkoutPlanRepository : IGenericRepository<WorkoutPlan>
{
    Task<IEnumerable<WorkoutPlan>> GetByUserIdAsync(Guid userId);
}