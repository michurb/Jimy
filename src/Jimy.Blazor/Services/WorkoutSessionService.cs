using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
        var response = await _httpClient.GetAsync($"api/workout-sessions/{sessionId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<WorkoutSessionDto>();
    }

    public async Task UpdateExerciseWeight(Guid sessionId, Guid exerciseId, int setNumber, decimal weight)
    {
        var updateDto = new UpdateWorkoutSessionExerciseDto
        {
            SetNumber = setNumber,
            Weight = weight
        };
        var response = await _httpClient.PostAsJsonAsync($"api/workout-sessions/{sessionId}/exercises/{exerciseId}/set/{setNumber}/weight", updateDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task<Guid> StartWorkoutSession(Guid workoutPlanId)
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("No authentication token found.");
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var startSessionDto = new StartWorkoutSessionDto
        {
            WorkoutPlanId = workoutPlanId
        };

        var response = await _httpClient.PostAsJsonAsync("api/workout-sessions/start", startSessionDto);
        
        if (response.IsSuccessStatusCode)
        {
            var sessionId = await response.Content.ReadFromJsonAsync<Guid>();
            return sessionId;
        }

        throw new Exception($"Failed to start workout session: {response.StatusCode}");
    }
    
    public async Task EndWorkoutSessionAsync(Guid sessionId)
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("No authentication token found.");
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsync($"api/workout-sessions/{sessionId}/end", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task<WorkoutSessionDto> GetActiveWorkoutSessionAsync()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
    
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("No authentication token found.");
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    
        var response = await _httpClient.GetAsync("api/workout-sessions/active");
    
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }
            return JsonSerializer.Deserialize<WorkoutSessionDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        throw new Exception($"Failed to get active workout session: {response.StatusCode}");
    }
}