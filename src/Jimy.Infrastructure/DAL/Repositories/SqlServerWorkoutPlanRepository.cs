using Jimy.Core.Entities;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Repositories;

internal sealed class SqlServerWorkoutPlanRepository : IWorkoutPlanRepository
{
    private readonly DbSet<WorkoutPlan> _workoutPlans;
    private readonly JimyDbContext _dbContext;

    public SqlServerWorkoutPlanRepository(JimyDbContext dbContext)
    {
        _workoutPlans = dbContext.WorkoutPlans;
        _dbContext = dbContext;
    }
    public async Task<WorkoutPlan> GetByIdAsync(WorkoutPlanId id)
        => await _workoutPlans
            .Include(x => x.Exercises)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<WorkoutPlan>> GetByUserIdAsync(UserId userId)
        => await _workoutPlans
            .Include(x => x.Exercises)
            .Where(x => x.UserId == userId)
            .ToListAsync();

    public async Task AddAsync(WorkoutPlan workoutPlan)
        => await _workoutPlans.AddAsync(workoutPlan);

    public Task UpdateAsync(WorkoutPlan workoutPlan)
    {
        _workoutPlans.Update(workoutPlan);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(WorkoutPlanId id)
    {
        var workoutPlan = await GetByIdAsync(id);
        _workoutPlans.Remove(workoutPlan);
    }
}