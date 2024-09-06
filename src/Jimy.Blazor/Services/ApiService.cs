using System.Net.Http.Json;
using Jimy.Business.DTOs;

namespace Jimy.Blazor.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5103/api/");
    }

    // Users
    public async Task<IEnumerable<UserDto>> GetUsersAsync() =>
        await _httpClient.GetFromJsonAsync<IEnumerable<UserDto>>("users") ?? Array.Empty<UserDto>();

    public async Task<UserDto> GetUserAsync(Guid id) =>
        await _httpClient.GetFromJsonAsync<UserDto>($"users/{id}") ?? throw new Exception("User not found");

    public async Task CreateUserAsync(CreateUserDto user) =>
        await _httpClient.PostAsJsonAsync("users", user);

    public async Task UpdateUserAsync(Guid id, UpdateUserDto user) =>
        await _httpClient.PutAsJsonAsync($"users/{id}", user);

    public async Task DeleteUserAsync(Guid id) =>
        await _httpClient.DeleteAsync($"users/{id}");

    // WorkoutPlans
    public async Task<IEnumerable<WorkoutPlanDto>> GetWorkoutPlansAsync(Guid userId) =>
        await _httpClient.GetFromJsonAsync<IEnumerable<WorkoutPlanDto>>($"workoutplans/user/{userId}") ?? Array.Empty<WorkoutPlanDto>();

    public async Task<WorkoutPlanDto> GetWorkoutPlanAsync(int id) =>
        await _httpClient.GetFromJsonAsync<WorkoutPlanDto>($"workoutplans/{id}") ?? throw new Exception("Workout plan not found");

    public async Task CreateWorkoutPlanAsync(CreateWorkoutPlanDto workoutPlan) =>
        await _httpClient.PostAsJsonAsync("workoutplans", workoutPlan);

    public async Task UpdateWorkoutPlanAsync(int id, UpdateWorkoutPlanDto workoutPlan) =>
        await _httpClient.PutAsJsonAsync($"workoutplans/{id}", workoutPlan);

    public async Task DeleteWorkoutPlanAsync(int id) =>
        await _httpClient.DeleteAsync($"workoutplans/{id}");

    // Add similar methods for Exercises and ActivityLogs
}