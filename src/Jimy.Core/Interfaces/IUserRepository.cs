using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;

namespace Jimy.Core.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task<User> GetByEmailAsync(Email email);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(UserId id);
    Task<User> GetByUsernameAsync(Username username);
}