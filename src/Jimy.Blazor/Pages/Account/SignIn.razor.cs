using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Jimy.Blazor.Pages.Account;

public partial class SignIn : ComponentBase
{
    [Inject] private IAuthService AuthService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IJSRuntime JSRuntime { get; set; }

    private SignInDto signInModel = new SignInDto();
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
                await JSRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", response.AccessToken);
                NavigationManager.NavigateTo(string.IsNullOrEmpty(returnUrl) ? "/dashboard" : returnUrl);
            }
            else
            {
                errorMessage = "Invalid response from server";
            }
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }
    }
}