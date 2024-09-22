using System.Net.Http.Headers;
using System.Net.Http.Json;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Models;
using Microsoft.JSInterop;

namespace Jimy.Blazor.Services;

public class ActivityLogService : IActivityLogService
{    
    private readonly IHttpClientFactory _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public ActivityLogService(IHttpClientFactory httpClientFactory, IJSRuntime jsRuntime)
    {
        _httpClient = httpClientFactory;
        _jsRuntime = jsRuntime;
    }

    public async Task<List<ActivityLogDto>> GetUserActivityLogsAsync()
    {
        var client = _httpClient.CreateClient("MainApi");
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("No authentication token found.");
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await client.GetAsync($"api/activity-logs/user");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<ActivityLogDto>>();
        }

        throw new Exception($"Failed to get activity logs: {response.StatusCode}");
    }

    public async Task CreateActivityLogAsync(CreateActivityLogDto activityLog)
    {
        var client = _httpClient.CreateClient("MainApi");
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("No authentication token found.");
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await client.PostAsJsonAsync("api/activity-logs", activityLog);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to create activity log: {errorContent}");
        }
    }

    public async Task DeleteActivityLogAsync(Guid activityLogId)
    {
        var client = _httpClient.CreateClient("MainApi");
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.DeleteAsync($"api/activity-logs/{activityLogId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateActivityLogAsync(ActivityLogDto activityLog)
    {
        var client = _httpClient.CreateClient("MainApi");
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        

        var response = await client.PutAsJsonAsync($"api/activity-logs/{activityLog.Id}", activityLog);
        response.EnsureSuccessStatusCode();
    }
}
