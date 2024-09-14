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

    public async Task UpdateAsync(WorkoutPlan workoutPlan)
    {
        _dbContext.Entry(workoutPlan).State = EntityState.Modified;
        
        var existingExercises = await _dbContext.Set<WorkoutExercise>()
            .Where(we => EF.Property<WorkoutPlanId>(we, "WorkoutPlanId") == workoutPlan.Id)
            .ToListAsync();
        _dbContext.Set<WorkoutExercise>().RemoveRange(existingExercises);
        
        foreach (var exercise in workoutPlan.Exercises)
        {
            _dbContext.Entry(exercise).State = EntityState.Added;
        }

        _workoutPlans.Update(workoutPlan);
    }

    public async Task DeleteAsync(WorkoutPlanId id)
    {
        var workoutPlan = await _workoutPlans.Include(wp => wp.Exercises)
            .SingleOrDefaultAsync(wp => wp.Id == id);
        foreach (var exercise in workoutPlan.Exercises)
        {
            _dbContext.Entry(exercise).State = EntityState.Deleted;
        }

        _workoutPlans.Remove(workoutPlan);
    }
}