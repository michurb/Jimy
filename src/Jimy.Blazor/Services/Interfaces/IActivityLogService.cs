using Jimy.Blazor.Models;

namespace Jimy.Blazor.Services.Interfaces;

public interface IActivityLogService
{
    Task<IEnumerable<ActivityLogDto>> GetUserActivityLogsAsync();
    Task CreateActivityLogAsync(CreateActivityLogDto activityLog);
    Task DeleteActivityLogAsync(Guid activityLogId);
    Task UpdateActivityLogAsync(ActivityLogDto activityLog);
}