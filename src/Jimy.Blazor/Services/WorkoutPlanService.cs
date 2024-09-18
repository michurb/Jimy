using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Jimy.Blazor.Models;
using Microsoft.JSInterop;

namespace Jimy.Blazor.API.Interfaces;

public class WorkoutPlanService : IWorkoutPlanService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public WorkoutPlanService(IHttpClientFactory httpClientFactory, IJSRuntime jsRuntime)
    {
        _httpClient = httpClientFactory;
        _jsRuntime = jsRuntime;
    }
    public async Task CreateWorkoutPlanAsync(CreateWorkoutPlanDto workoutPlan)
    {
        var client = _httpClient.CreateClient("MainApi");
        var response = await client.PostAsJsonAsync("api/workout-plans", workoutPlan);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to create workout plan: {errorContent}");
        }
    }

    public async Task<List<WorkoutPlanDto>> GetUserWorkoutPlansAsync()
    {
        var client = _httpClient.CreateClient("MainApi");
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("No authentication token found.");
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await client.GetAsync("/api/workout-plans/user");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<WorkoutPlanDto>>();    
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException("Invalid or expired token.");
        }
        
        throw new Exception($"Failed to get current user: {response.StatusCode}");
    }

    public async Task UpdateWorkoutPlanAsync(WorkoutPlanDto workoutPlan)
    {
        var client = _httpClient.CreateClient("MainApi");
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("No authentication token found.");
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await client.PutAsJsonAsync($"api/workout-plans/{workoutPlan.Id}", workoutPlan);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to update workout plan: {errorContent}");
        }
    }

    public async Task DeleteWorkoutPlanAsync(Guid workoutPlanId)
    {
        var client = _httpClient.CreateClient("MainApi");
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("No authentication token found.");
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await client.DeleteAsync($"api/workout-plans/{workoutPlanId}");
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to delete workout plan: {errorContent}");
        }
    }

    public async Task<WorkoutPlanDto> GetWorkoutPlanAsync(Guid workoutPlanId)
    {
        var client = _httpClient.CreateClient("MainApi");
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("No authentication token found.");
        }
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await client.GetAsync($"api/workout-plans/{workoutPlanId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<WorkoutPlanDto>();
        }

        throw new Exception("cannot get workoutplan");
    }
}