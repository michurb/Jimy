using Jimy.Data.Entities;

namespace Jimy.Data.Interfaces;

public interface IActivityLogRepository : IGenericRepository<ActivityLog>
{
    Task<IEnumerable<ActivityLog>> GetByUserIdAsync(int userId);
}