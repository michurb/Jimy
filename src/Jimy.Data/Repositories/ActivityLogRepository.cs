using Jimy.Data.Data;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Data.Repositories;

public class ActivityLogRepository : GenericRepository<ActivityLog>, IActivityLogRepository
{
    public ActivityLogRepository(JimyDbContext context) : base(context) { }

    public async Task<IEnumerable<ActivityLog>> GetByUserIdAsync(int userId)
    {
        return await _dbSet.Where(al => al.UserId == userId).ToListAsync();
    }
}