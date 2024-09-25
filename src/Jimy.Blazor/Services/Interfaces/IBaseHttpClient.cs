namespace Jimy.Blazor.API.Interfaces;

public interface IBaseHttpClient
{
    Task<HttpClient> GetClientAsync();
}