using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;

namespace Jimy.Core.Interfaces;

public interface IActivityLogRepository
{
    Task<ActivityLog> GetByIdAsync(ActivityLogId id);
    Task<IEnumerable<ActivityLog>> GetByUserIdAsync(UserId userId);
    Task AddAsync(ActivityLog activityLog);
    Task UpdateAsync(ActivityLog activityLog);
    Task DeleteAsync(ActivityLogId id);
}