using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Jimy.Blazor.Exceptions;
using Jimy.Blazor.Models;
using Jimy.Blazor.Services.Interfaces;
using Microsoft.JSInterop;

namespace Jimy.Blazor.API.Interfaces;

public class WorkoutPlanService : IWorkoutPlanService
{
    private readonly IBaseHttpClient _baseHttpClient;

    public WorkoutPlanService(IBaseHttpClient baseHttpClient)
    {
        _baseHttpClient = baseHttpClient;
    }

    public async Task CreateWorkoutPlanAsync(CreateWorkoutPlanDto workoutPlan)
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.PostAsJsonAsync("api/workout-plans", workoutPlan);
        if (!response.IsSuccessStatusCode)
        {
            throw new FailedToCreateWorkoutPlanException();
        }
    }

    public async Task<IEnumerable<WorkoutPlanDto>> GetUserWorkoutPlansAsync()
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.GetAsync("/api/workout-plans/user");
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException("Invalid or expired token.");
        }
        return await response.Content.ReadFromJsonAsync<List<WorkoutPlanDto>>();  
    }

    public async Task UpdateWorkoutPlanAsync(WorkoutPlanDto workoutPlan)
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.PutAsJsonAsync($"api/workout-plans/{workoutPlan.Id}", workoutPlan);
        if (!response.IsSuccessStatusCode)
        {
            throw new FailedToUpdateWorkoutPlanException();
        }
    }

    public async Task DeleteWorkoutPlanAsync(Guid workoutPlanId)
    {
        var client = await _baseHttpClient.GetClientAsync();
        
        var response = await client.DeleteAsync($"api/workout-plans/{workoutPlanId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new FailedToDeleteWorkoutPlanException();
        }
    }

    public async Task<WorkoutPlanDto> GetWorkoutPlanAsync(Guid workoutPlanId)
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.GetAsync($"api/workout-plans/{workoutPlanId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<WorkoutPlanDto>();
        }

        throw new CouldNotFindWorkoutPlanException();
    }

    public async Task<IEnumerable<WorkoutPlanDto>> GetAllWorkoutPlansAsync()
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.GetAsync("api/workout-plans");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<WorkoutPlanDto>>();
        }

        throw new CouldNotFindWorkoutPlanException();
    }
}