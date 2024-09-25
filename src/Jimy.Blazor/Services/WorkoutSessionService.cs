using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Exceptions;
using Jimy.Blazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Jimy.Blazor.Services;

public class WorkoutSessionService : IWorkoutSessionService
{
    private readonly IBaseHttpClient _baseHttpClient;

    public WorkoutSessionService(IBaseHttpClient baseHttpClient)
    {
        _baseHttpClient = baseHttpClient;
    }
    
    public async Task<WorkoutSessionDto> GetSessionAsync(Guid sessionId)
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.GetAsync($"api/workout-sessions/{sessionId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<WorkoutSessionDto>();
    }

    public async Task UpdateExerciseWeight(Guid sessionId, Guid exerciseId, int setNumber, decimal weight)
    {
        var client = await _baseHttpClient.GetClientAsync();
        var updateDto = new UpdateWorkoutSessionExerciseDto
        {
            SetNumber = setNumber,
            Weight = weight
        };
        var response = await client.PostAsJsonAsync($"api/workout-sessions/{sessionId}/exercises/{exerciseId}/set/{setNumber}/weight", updateDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task<Guid> StartWorkoutSession(Guid workoutPlanId)
    {
        var client = await _baseHttpClient.GetClientAsync();
        var startSessionDto = new StartWorkoutSessionDto
        {
            WorkoutPlanId = workoutPlanId
        };

        var response = await client.PostAsJsonAsync("api/workout-sessions/start", startSessionDto);
        
        if (response.IsSuccessStatusCode)
        {
            var sessionId = await response.Content.ReadFromJsonAsync<Guid>();
            return sessionId;
        }

        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            throw new AlreadyStartedWrongWorkoutException();
        }
        throw new CouldNotStartWorkoutSeesionException();
    }
    
    public async Task EndWorkoutSessionAsync(Guid sessionId)
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.PostAsync($"api/workout-sessions/{sessionId}/end", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task<WorkoutSessionDto> GetActiveWorkoutSessionAsync()
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.GetAsync("api/workout-sessions/active");
    
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

        throw new CouldNotGetActiveWorkoutSessionException();
    }
}