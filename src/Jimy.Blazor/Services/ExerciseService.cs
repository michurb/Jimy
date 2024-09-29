using System.Net.Http.Json;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Models;
using Jimy.Blazor.Services.Interfaces;

namespace Jimy.Blazor.Services;

public class ExerciseService : IExerciseService
{
    private readonly IBaseHttpClient _baseHttpClient;

    public ExerciseService(IBaseHttpClient baseHttpClient)
    {
        _baseHttpClient = baseHttpClient;
    }

    public async Task<IEnumerable<ExerciseDto>> GetExercisesAsync()
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.GetAsync("api/exercises");
        return await response.Content.ReadFromJsonAsync<List<ExerciseDto>>();
    }

    public async Task CreateExerciseAsync(CreateExerciseDto createExerciseDto)
    {
      var client = await _baseHttpClient.GetClientAsync();
      var response = await client.PostAsJsonAsync("api/exercises", createExerciseDto);
      response.EnsureSuccessStatusCode();
    }

    public async Task UpdateExerciseAsync(EditExerciseDto editExerciseDto)
    {
      var client = await _baseHttpClient.GetClientAsync();
      var response = await client.PutAsJsonAsync($"api/exercises/{editExerciseDto.Id}", editExerciseDto);
      response.EnsureSuccessStatusCode();
    }

    public async Task DeleteExerciseAsync(Guid exerciseId)
    {
      var client = await _baseHttpClient.GetClientAsync();
      var response = await client.DeleteAsync($"api/exercises/{exerciseId}");
      response.EnsureSuccessStatusCode();
    }
}