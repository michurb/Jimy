using Jimy.Business.DTOs;

namespace Jimy.Business.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    Task DeleteUserAsync(int id);
}