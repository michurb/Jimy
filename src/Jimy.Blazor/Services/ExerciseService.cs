using System.Net.Http.Json;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Models;

namespace Jimy.Blazor.Services;

public class ExerciseService : IExerciseService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly string _baseUrl;

    public ExerciseService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
       _httpClient = httpClientFactory;
    }

    public async Task<List<ExerciseDto>> GetExercisesAsync()
    {
        var client = _httpClient.CreateClient("MainApi");
        var response = await client.GetAsync("api/exercises");
        return await response.Content.ReadFromJsonAsync<List<ExerciseDto>>();
    }
}