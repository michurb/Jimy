using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Jimy.Blazor.API.Interfaces;

namespace Jimy.Blazor.Services;

public class BaseHttpClient : IBaseHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public BaseHttpClient(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage)
    {
        _httpClient = httpClientFactory.CreateClient("MainApi");
        _localStorage = localStorage;
    }

    public async Task<HttpClient> GetClientAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return _httpClient;
    }
}