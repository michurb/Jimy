using Jimy.Data.Entities;

namespace Jimy.Data.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByEmailAsync(string email);
    Task DeleteAsync(Guid id);
}