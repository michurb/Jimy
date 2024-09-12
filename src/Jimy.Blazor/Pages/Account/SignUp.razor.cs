using Jimy.Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace Jimy.Blazor.Pages.Account;

public partial class SignUp : ComponentBase
{
    private SignUpDto signUpModel = new SignUpDto();

    private async Task HandleValidSubmit()
    {
        try
        {
            var response = await AuthService.SignUpAsync(signUpModel);
            // Handle successful sign-up (e.g., store the token, redirect to dashboard)
            NavigationManager.NavigateTo("/dashboard");
        }
        catch (Exception ex)
        {
            // Handle error (e.g., show error message)
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}