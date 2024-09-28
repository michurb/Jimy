using Jimy.Blazor.Models;

namespace Jimy.Blazor.Services.Interfaces;

public interface IAdminService
{
  Task<UserDto> GetUserAsync(Guid userId);
  Task<IEnumerable<UserDto>> GetUsersAsync();
  Task<DashboardData?> GetDashboardDataAsync();
  Task CreateUserAsync(CreateUserDto createUserDto);
  Task UpdateUserAsync(EditUserDto editUserDto);
  Task DeleteUserAsync(Guid userId);
}