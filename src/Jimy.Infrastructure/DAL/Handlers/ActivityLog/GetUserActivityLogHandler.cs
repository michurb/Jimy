using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.ActivityLogs;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers.ActivityLog;

internal sealed class GetUserActivityLogHandler : IQueryHandler<GetUserActivityLog, IEnumerable<ActivityLogDto>>
{
    private readonly JimyDbContext _dbContext;

    public GetUserActivityLogHandler(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<ActivityLogDto>> HandleAsync(GetUserActivityLog query)
    {
        var userId = new UserId(query.UserId);
        var userActivityLogs = await _dbContext.ActivityLogs
            .AsNoTracking()
            .Where(al => al.UserId == userId)
            .Select(al => al.AsDto())
            .ToListAsync();
        return userActivityLogs;
    }
}