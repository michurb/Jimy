using System.Net.Http.Json;
using System.Text.Json;
using Jimy.Blazor.Models;

namespace Jimy.Blazor.API.Interfaces;

public class WorkoutPlanService : IWorkoutPlanService
{
    private readonly IHttpClientFactory _httpClient;

    public WorkoutPlanService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory;
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
        var response = await client.GetAsync("/api/workout-plans/user");
        return await response.Content.ReadFromJsonAsync<List<WorkoutPlanDto>>();
    }
}