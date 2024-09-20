using Jimy.Blazor.Models;

namespace Jimy.Blazor.API.Interfaces;

public interface IActivityLogService
{
    Task<List<ActivityLogDto>> GetUserActivityLogsAsync();
    Task CreateActivityLogAsync(CreateActivityLogDto activityLog);
}