using Jimy.Data.Data;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Data.Repositories;

public class WorkoutPlanRepository : GenericRepository<WorkoutPlan>, IWorkoutPlanRepository
{
    public WorkoutPlanRepository(JimyDbContext context) : base(context) { }

    public async Task<IEnumerable<WorkoutPlan>> GetByUserIdAsync(int userId)
    {
        return await _dbSet.Where(wp => wp.UserId == userId).ToListAsync();
    }

    public override async Task<WorkoutPlan> AddAsync(WorkoutPlan workoutPlan)
    {
        workoutPlan.CreatedDate = DateTime.UtcNow;
        return await base.AddAsync(workoutPlan);
    }
}