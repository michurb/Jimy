using System.Net.Http.Json;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Models;

namespace Jimy.Blazor.Services;

public class ExerciseService : IExerciseService
{
    private readonly IBaseHttpClient _baseHttpClient;

    public ExerciseService(IBaseHttpClient baseHttpClient)
    {
        _baseHttpClient = baseHttpClient;
    }

    public async Task<List<ExerciseDto>> GetExercisesAsync()
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.GetAsync("api/exercises");
        return await response.Content.ReadFromJsonAsync<List<ExerciseDto>>();
    }
}