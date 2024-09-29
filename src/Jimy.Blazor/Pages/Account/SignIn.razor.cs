using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Models;
using Jimy.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Jimy.Blazor.Pages.Account;

public partial class SignIn : ComponentBase
{
    [Inject] private IAuthService AuthService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private SignInDto signInModel = new();
    private string errorMessage = string.Empty;
    private string returnUrl = string.Empty;

    protected override void OnInitialized()
    {
        var uri = new Uri(NavigationManager.Uri);
        returnUrl = Uri.UnescapeDataString(uri.Query.TrimStart('?').Split('=').LastOrDefault() ?? string.Empty);
    }

    private async Task HandleValidSubmit()
    {
        try
        {
            errorMessage = string.Empty;
            var response = await AuthService.SignInAsync(signInModel);
            if (!string.IsNullOrEmpty(response.AccessToken))
            {
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                NavigationManager.NavigateTo(string.IsNullOrEmpty(returnUrl) ? "/dashboard" : returnUrl);
            }
            else
            {
                errorMessage = "Invalid email or password";
            }
        }
        catch (Exception ex)
        {
            errorMessage = "Invalid email or password";
        }
    }
}