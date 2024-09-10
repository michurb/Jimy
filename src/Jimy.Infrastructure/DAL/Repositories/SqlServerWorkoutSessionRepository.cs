using Jimy.Core.Entities;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Repositories;

internal sealed class SqlServerWorkoutSessionRepository : IWorkoutSessionRepository
{
    private readonly DbSet<WorkoutSession> _workoutSessions;
    private readonly JimyDbContext _dbContext;

    public SqlServerWorkoutSessionRepository(JimyDbContext dbContext)
    {
        _workoutSessions = _dbContext.WorkoutSessions;
        _dbContext = dbContext;
    }

    public async Task<WorkoutSession> GetByIdAsync(WorkoutSessionId id)
        => await _workoutSessions
            .Include(x => x.Exercises)
            .SingleOrDefaultAsync(x => x.Id == id);
    public async Task<IEnumerable<WorkoutSession>> GetByUserIdAsync(UserId userId)
        => await _workoutSessions
            .Include(x => x.Exercises)
            .Where(x => x.UserId == userId)
            .ToListAsync();
    public async Task AddAsync(WorkoutSession workoutSession)
        => await _workoutSessions.AddAsync(workoutSession);

    public Task UpdateAsync(WorkoutSession workoutSession)
    {
        _workoutSessions.Update(workoutSession);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(WorkoutSessionId id)
    {
        var workoutSession = await GetByIdAsync(id);
        _workoutSessions.Remove(workoutSession);
    }
}