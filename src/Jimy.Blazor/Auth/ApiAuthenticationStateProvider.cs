using System.Security.Claims;
using System.Text.Json;
using Jimy.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Jimy.Blazor.Auth;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiAuthenticationStateProvider(IJSRuntime jsRuntime, IHttpClientFactory httpClientFactory)
    {
        _jsRuntime = jsRuntime;
        _httpClientFactory = httpClientFactory;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        try
        {
            var user = await GetUserFromApi(token);
            var identity = CreateClaimsIdentity(user);
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    public async Task MarkUserAsAuthenticated(UserDto user)
    {
        var identity = CreateClaimsIdentity(user);
        var claimsPrincipal = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public void MarkUserAsLoggedOut()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
    }

    private ClaimsIdentity CreateClaimsIdentity(UserDto user)
    {
        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        }, "apiauth_type");

        return claimsIdentity;
    }

    private async Task<UserDto> GetUserFromApi(string token)
    {
        var client = _httpClientFactory.CreateClient("MainApi");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("api/users/me");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}