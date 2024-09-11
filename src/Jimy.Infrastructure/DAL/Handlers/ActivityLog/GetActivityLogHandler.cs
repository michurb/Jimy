using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.ActivityLogs;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers.ActivityLog;

internal sealed class GetActivityLogHandler : IQueryHandler<GetActivityLog, ActivityLogDto>
{
    private readonly JimyDbContext _dbContext;

    public GetActivityLogHandler(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ActivityLogDto> HandleAsync(GetActivityLog query)
    {
        var activityLogId = new ActivityLogId(query.ActivityLogId);
        var activityLog = await _dbContext.ActivityLogs
            .AsNoTracking()
            .SingleOrDefaultAsync(al => al.Id == activityLogId);
        return activityLog?.AsDto();
    }
}