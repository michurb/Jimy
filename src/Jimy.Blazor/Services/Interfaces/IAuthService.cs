using Jimy.Blazor.Models;

namespace Jimy.Blazor.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> SignInAsync(SignInDto signInDto);
    Task<AuthResponseDto> SignUpAsync(SignUpDto signUpDto);
    Task<UserDto> GetCurrentUserAsync();
    Task SignOutAsync();
    Task<bool> IsAdminAsync();
}