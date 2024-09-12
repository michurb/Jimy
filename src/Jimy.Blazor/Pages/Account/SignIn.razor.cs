using System.Net.Http.Json;
using Jimy.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Jimy.Blazor.Pages.Account;

public partial class SignIn : ComponentBase
{
    private SignInDto signInModel = new SignInDto();
    private string errorMessage = string.Empty;

    private async Task HandleValidSubmit()
    {
        try
        {
            errorMessage = string.Empty;
            var response = await AuthService.SignInAsync(signInModel);
            if (!string.IsNullOrEmpty(response.AccessToken))
            {
                await JSRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", response.AccessToken);
                NavigationManager.NavigateTo("/dashboard");
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