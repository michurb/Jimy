using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Exceptions;
using Jimy.Blazor.Models;
using Microsoft.JSInterop;

namespace Jimy.Blazor.Services;

public class ActivityLogService : IActivityLogService
{
    private readonly IBaseHttpClient _baseHttpClient;

    public ActivityLogService(IBaseHttpClient baseHttpClient)
    {
        _baseHttpClient = baseHttpClient;
    }

    public async Task<List<ActivityLogDto>> GetUserActivityLogsAsync()
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.GetAsync($"api/activity-logs/user");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<ActivityLogDto>>();
        }

        throw new FailedToGetCurrentUserActvity();
    }

    public async Task CreateActivityLogAsync(CreateActivityLogDto activityLog)
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.PostAsJsonAsync("api/activity-logs", activityLog);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new FailedToCreateActityLogException();
        }
    }

    public async Task DeleteActivityLogAsync(Guid activityLogId)
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.DeleteAsync($"api/activity-logs/{activityLogId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateActivityLogAsync(ActivityLogDto activityLog)
    {
        var client = await _baseHttpClient.GetClientAsync();
        var response = await client.PutAsJsonAsync($"api/activity-logs/{activityLog.Id}", activityLog);
        response.EnsureSuccessStatusCode();
    }
}
