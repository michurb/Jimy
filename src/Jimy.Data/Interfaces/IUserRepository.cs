using Jimy.Data.Entities;

namespace Jimy.Data.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByEmailAsync(string email);
}