using Jimy.Data.Data;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Data.Repositories;

public class WorkoutSessionRepository :
    GenericRepository<WorkoutSession>, IWorkoutSessionRepository
{
    public WorkoutSessionRepository(JimyDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<WorkoutSession>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(ws => ws.UserId == userId).ToListAsync();
    }

    public async Task<WorkoutSession> GetWithExercisesAsync(int id)
    {
        return await _dbSet
            .Include(ws => ws.WorkoutPlan)
            .Include(ws => ws.Exercises)
            .ThenInclude(wse => wse.Exercise)
            .FirstOrDefaultAsync(ws => ws.Id == id);
    }
}