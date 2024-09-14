using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Auth;
using Jimy.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Jimy.Blazor.Services;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(IHttpClientFactory httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }
    public async Task<AuthResponseDto> SignInAsync(SignInDto signInDto)
    {
        var client = _httpClient.CreateClient("MainApi");
        var response = await client.PostAsJsonAsync("api/users/sign-in", signInDto);
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<AuthResponseDto>(content, options);
        }
        
        // If the response is not successful, throw an exception with the error message
        var errorContent = await response.Content.ReadAsStringAsync();
        throw new Exception($"Authentication failed: {errorContent}");
    }

    public async Task<AuthResponseDto> SignUpAsync(SignUpDto signUpDto)
    {
        var client = _httpClient.CreateClient("MainApi");
        
        var response = await client.PostAsJsonAsync("api/users", signUpDto);
        
        if (response.IsSuccessStatusCode)
        {
            // For sign up, we might need to sign in afterwards to get the token
            return await SignInAsync(new SignInDto { Email = signUpDto.Email, Password = signUpDto.Password });
        }
        
        var errorContent = await response.Content.ReadAsStringAsync();
        throw new Exception($"Registration failed: {errorContent}");
    }

    public async Task<UserDto> GetCurrentUserAsync()
    {
        var client = _httpClient.CreateClient("MainApi");
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("No authentication token found.");
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await client.GetAsync("api/users/me");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
        
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException("Invalid or expired token.");
        }
        
        throw new Exception($"Failed to get current user: {response.StatusCode}");
    }
    
    public async Task SignOutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
        ((ApiAuthenticationStateProvider)_authStateProvider).MarkUserAsLoggedOut();
    }
}