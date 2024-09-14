using Jimy.Blazor;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("MainApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5080/");
});

builder.Services.AddHttpClient("SecondaryApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7008/");
});

builder.Services.AddScoped<IAuthService, AuthService>();


await builder.Build().RunAsync();