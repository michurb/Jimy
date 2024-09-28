using Blazored.LocalStorage;
using Jimy.Blazor;
using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Auth;
using Jimy.Blazor.Services;
using Jimy.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpClient("MainApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5080/");
});


builder.Services.AddHttpClient("SecondaryApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7008/");
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<IWorkoutSessionService, WorkoutSessionService>();
builder.Services.AddScoped<IActivityLogService, ActivityLogService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IBaseHttpClient, BaseHttpClient>();
builder.Services.AddScoped<IAdminService, AdminService>();



await builder.Build().RunAsync();