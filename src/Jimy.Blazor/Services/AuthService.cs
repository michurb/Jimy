using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Auth;
using Jimy.Blazor.Exceptions;
using Jimy.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Jimy.Blazor.Services;

public class AuthService : IAuthService
{
    private readonly IBaseHttpClient _baseHttpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(IBaseHttpClient baseHttpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
    {
        _baseHttpClient = baseHttpClient;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
    }

    public async Task<AuthResponseDto> SignInAsync(SignInDto signInDto)
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.PostAsJsonAsync("api/users/sign-in", signInDto);
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<AuthResponseDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            await _localStorage.SetItemAsync("authToken", authResponse.AccessToken);
            await ((ApiAuthenticationStateProvider)_authStateProvider).MarkUserAsAuthenticated(authResponse.AccessToken);
        
            return authResponse;
        }
        
        var errorContent = await response.Content.ReadAsStringAsync();
        throw new  UnauthorizedAccessException($"Authentication failed: {errorContent}");
    }

    public async Task<AuthResponseDto> SignUpAsync(SignUpDto signUpDto)
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.PostAsJsonAsync("api/users", signUpDto);
        
        if (response.IsSuccessStatusCode)
        {
            return await SignInAsync(new SignInDto { Email = signUpDto.Email, Password = signUpDto.Password });
        }
        
        var errorContent = await response.Content.ReadAsStringAsync();
        throw new RegistrationFailedException();
    }

    public async Task<UserDto> GetCurrentUserAsync()
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.GetAsync("api/users/me");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
        
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException("Invalid or expired token.");
        }

        throw new FailedToGetCurrentUserUserException();
    }
    public async Task SignOutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await ((ApiAuthenticationStateProvider)_authStateProvider).MarkUserAsLoggedOut();
    }

    public async Task<bool> IsAdminAsync()
    {
      var user = await GetCurrentUserAsync();
      return user.Role == "admin";
    }
}