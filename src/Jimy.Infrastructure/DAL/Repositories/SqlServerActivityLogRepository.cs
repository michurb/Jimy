using Jimy.Core.Entities;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Repositories;

internal sealed class SqlServerActivityLogRepository : IActivityLogRepository
{
    private readonly DbSet<ActivityLog> _activityLogs;
    private readonly JimyDbContext _dbContext;

    public SqlServerActivityLogRepository(JimyDbContext dbContext)
    {
        _activityLogs = dbContext.ActivityLogs;
        _dbContext = dbContext;
    }

    public async Task<ActivityLog> GetByIdAsync(ActivityLogId id)
        => await _activityLogs.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<ActivityLog>> GetByUserIdAsync(UserId userId)
        => await _activityLogs
            .Where(x => x.UserId == userId)
            .ToListAsync();

    public async Task AddAsync(ActivityLog activityLog)
        => await _activityLogs.AddAsync(activityLog);

    public Task UpdateAsync(ActivityLog activityLog)
    {
        _activityLogs.Update(activityLog);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(ActivityLogId id)
    {
        var activityLog = await GetByIdAsync(id);
        _activityLogs.Remove(activityLog);
    }
}