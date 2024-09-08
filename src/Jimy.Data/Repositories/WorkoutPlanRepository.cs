using Jimy.Data.Data;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Data.Repositories;

public class WorkoutPlanRepository : GenericRepository<WorkoutPlan>, IWorkoutPlanRepository
{
    public WorkoutPlanRepository(JimyDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<WorkoutPlan>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Include(wp => wp.Exercises)
            .ThenInclude(we => we.Exercise)
            .Where(wp => wp.UserId == userId)
            .ToListAsync();
    }

    public async Task<WorkoutPlan> GetByIdWithExercisesAsync(int id)
    {
        return await _dbSet
            .Include(wp => wp.Exercises)
            .ThenInclude(we => we.Exercise)
            .FirstOrDefaultAsync(wp => wp.Id == id);
    }

    public override async Task<WorkoutPlan> AddAsync(WorkoutPlan workoutPlan)
    {
        workoutPlan.CreatedDate = DateTime.UtcNow;
        var entry = await _dbSet.AddAsync(workoutPlan);
        await _context.SaveChangesAsync();
        
        await _context.Entry(entry.Entity)
            .Collection(w => w.Exercises)
            .LoadAsync();

        return entry.Entity;
    }
}