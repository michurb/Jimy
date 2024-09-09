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

    // Exercises
    public async Task<IEnumerable<ExerciseDto>> GetExercisesAsync() =>
        await _httpClient.GetFromJsonAsync<IEnumerable<ExerciseDto>>("exercises") ?? Array.Empty<ExerciseDto>();
    
    public async Task<ExerciseDto> GetExerciseAsync(int id) =>
        await _httpClient.GetFromJsonAsync<ExerciseDto>($"exercises/{id}") ?? throw new Exception("Exercise not found");
    
    public async Task CreateExerciseAsync(CreateExerciseDto exercise) =>
        await _httpClient.PostAsJsonAsync("exercises", exercise);
    
    public async Task UpdateExerciseAsync(int id, UpdateExerciseDto exercise) =>
        await _httpClient.PutAsJsonAsync($"exercises/{id}", exercise);
    
    public async Task DeleteExerciseAsync(int id) =>
        await _httpClient.DeleteAsync($"exercises/{id}");
    
    //AcitvityLog
    
    public async Task<IEnumerable<ActivityLogDto>> GetActivityLogsAsync(Guid id) =>
        await _httpClient.GetFromJsonAsync<IEnumerable<ActivityLogDto>>($"activitylogs/user/{id}") ?? Array.Empty<ActivityLogDto>();
    
    public async Task<ActivityLogDto> GetActivityLogAsync(Guid id) =>
        await _httpClient.GetFromJsonAsync<ActivityLogDto>("activitylogs/user/{id}") ?? throw new Exception("Activity log not found");
    
    public async Task CreateActivityLogAsync(CreateActivityLogDto activityLog) =>
        await _httpClient.PostAsJsonAsync("activitylogs", activityLog);
    
    public async Task UpdateActivityLogAsync(int id, UpdateActivityLogDto activityLog) =>
        await _httpClient.PutAsJsonAsync($"activitylogs/{id}", activityLog);
    
    public async Task DeleteActivityLogAsync(int id) =>
        await _httpClient.DeleteAsync($"activitylogs/{id}");
    
    //WorkoutSession
    
    public async Task<WorkoutSessionDto> GetWorkoutSessionAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<WorkoutSessionDto>($"workoutsessions/{id}");
    }

    public async Task EndWorkoutSessionAsync(int id, List<WorkoutSessionExerciseDto> updatedExercises)
    {
        var response = await _httpClient.PostAsJsonAsync($"workoutsessions/{id}/end", updatedExercises);
        response.EnsureSuccessStatusCode();
    }
    
    public async Task<IEnumerable<WorkoutSessionDto>> GetUserWorkoutSessionsAsync(Guid userId)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<WorkoutSessionDto>>($"workoutsessions/user/{userId}");
    }
    
    public async Task<int> StartWorkoutSessionAsync(CreateWorkoutSessionDto workoutSession)
    {
        var response = await _httpClient.PostAsJsonAsync("workoutsessions/start", workoutSession);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>();
    }
    
}