using Jimy.Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace Jimy.Blazor.Pages.Account;

public partial class SignUp : ComponentBase
{
    private SignUpDto signUpModel = new SignUpDto();
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
            // Handle error (e.g., show error message)
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}