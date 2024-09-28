using System.Net.Http.Json;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Exceptions;
using Jimy.Blazor.Models;
using Jimy.Blazor.Services.Interfaces;

namespace Jimy.Blazor.Services;

internal sealed class AdminService : IAdminService
{
  private readonly IBaseHttpClient _baseHttpClient;

  public AdminService(IBaseHttpClient baseHttpClient)
  {
    _baseHttpClient = baseHttpClient;
  }
  
  public async Task<UserDto> GetUserAsync(Guid userId)
  {
    var client = await _baseHttpClient.GetClientAsync();
    var response = await client.GetAsync($"api/users/{userId}");
    
    if (response.IsSuccessStatusCode)
    {
      return await response.Content.ReadFromJsonAsync<UserDto>();
    }

    throw new UserNotFoundException(userId);
  }

  public async Task<IEnumerable<UserDto>> GetUsersAsync()
  {
    var client = await _baseHttpClient.GetClientAsync();
    var response = await client.GetAsync("api/users");

    if (response.IsSuccessStatusCode)
    {
      return await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>();
    }
    
    throw new FailedToGetUsersException();
  }

  public async Task<DashboardData?> GetDashboardDataAsync()
  {
    var client = await _baseHttpClient.GetClientAsync();
    var response = await client.GetAsync("api/admin/dashboard");
    return await response.Content.ReadFromJsonAsync<DashboardData>();
  }

  public async Task CreateUserAsync(CreateUserDto createUserDto)
  {
    var client = await _baseHttpClient.GetClientAsync();
    var response = await client.PostAsJsonAsync("api/users", createUserDto);
  }

  public async Task UpdateUserAsync(EditUserDto editUserDto)
  {
    var client = await _baseHttpClient.GetClientAsync();
    var response = await client.PutAsJsonAsync($"api/users/{editUserDto.Id}", editUserDto);
  }

  public async Task DeleteUserAsync(Guid userId)
  {
    var client = await _baseHttpClient.GetClientAsync();
    var response = await client.DeleteAsync($"api/users/{userId}");
  }
}