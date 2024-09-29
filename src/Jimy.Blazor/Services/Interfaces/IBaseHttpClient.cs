namespace Jimy.Blazor.Services.Interfaces;

public interface IBaseHttpClient
{
    Task<HttpClient> GetClientAsync();
}