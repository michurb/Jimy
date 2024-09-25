using Jimy.Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace Jimy.Blazor.Pages.Account;

public partial class SignUp : ComponentBase
{
    private SignUpDto signUpModel = new();
    private string errorMessage;

    private async Task HandleValidSubmit()
    {
        try
        {
            errorMessage = string.Empty;
            var response = await AuthService.SignUpAsync(signUpModel);
            NavigationManager.NavigateTo("/signin");
        }
        catch (Exception ex)
        {
            errorMessage = "An error occured during sing up. Please try again.";
        }
    }
}