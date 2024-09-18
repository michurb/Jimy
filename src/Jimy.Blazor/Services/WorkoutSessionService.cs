using System.Net.Http.Headers;
using System.Net.Http.Json;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Jimy.Blazor.Services;

public class WorkoutSessionService : IWorkoutSessionService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authProvider;
    private readonly IJSRuntime _jsRuntime;

    public WorkoutSessionService(IHttpClientFactory httpClientFactory, AuthenticationStateProvider authProvider,
        IJSRuntime jsRuntime)
    {
        _httpClient = httpClientFactory.CreateClient("MainApi");
        _authProvider = authProvider;
        _jsRuntime = jsRuntime;
    }
    
    public async Task<WorkoutSessionDto> GetSessionAsync(Guid sessionId)
    {
        var result = await _httpClient.GetFromJsonAsync<WorkoutSessionDto>($"api/workout-sessions/{sessionId}");
        return result;
    }

    public async Task UpdateExerciseWeight(Guid sessionId, Guid exerciseId, int setNumber, decimal weight)
    {
        var updateDto = new UpdateWorkoutSessionExerciseDto { SetNumber = setNumber, Weight = weight };
        await _httpClient.PostAsJsonAsync($"api/workout-sessions/{sessionId}/exercises/{exerciseId}/set/{setNumber}/weight", updateDto);
    }

    public async Task<Guid> StartWorkoutSession(Guid workoutPlanId)
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("No authentication token found.");
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.PostAsJsonAsync("api/workout-sessions/start-from-plan", new { WorkoutPlanId = workoutPlanId });
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        throw new Exception($"Failed to start workout session: {response.StatusCode}");
    }
    
    private async Task<string> GetTokenAsync()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity.IsAuthenticated)
        {
            var token = user.FindFirst(c => c.Type == "access_token")?.Value;
            return token;
        }

        return null;
    }
}