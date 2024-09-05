using Jimy.Data.Data;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Data.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(JimyDbContext context) : base(context)
    {
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = _dbSet.Find(id);
        _dbSet.Remove(user);
        await _context.SaveChangesAsync();
    }


    public override async Task<User> AddAsync(User user)
    {
        user.DateJoined = DateTime.UtcNow;
        return await base.AddAsync(user);
    }
}