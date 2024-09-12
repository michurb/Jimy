using System.Net.Http.Json;
using System.Text.Json;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Models;

namespace Jimy.Blazor.Services;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClient;

    public AuthService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
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
}