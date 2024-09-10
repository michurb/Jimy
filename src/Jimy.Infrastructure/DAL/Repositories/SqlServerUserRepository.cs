using Jimy.Core.Entities;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Repositories;

internal sealed class SqlServerUserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public SqlServerUserRepository(JimyDbContext dbContext)
    {
        _users = dbContext.Users;
    }
    public async Task<User> GetByIdAsync(UserId id) 
        => await _users.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<User> GetByEmailAsync(Email email)
        => await _users.SingleOrDefaultAsync(x => x.Email == email);

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _users.ToListAsync();

    public async Task AddAsync(User user)
        => await _users.AddAsync(user);

    public Task UpdateAsync(User user)
    {
        _users.Update(user);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(UserId id)
        => _users.Remove(await GetByIdAsync(id));
}