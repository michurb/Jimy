using Jimy.Blazor.API.Interfaces;
using Jimy.Blazor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Jimy.Blazor.Pages.Dashboard;

[Authorize]
public partial class Dashboard : ComponentBase
{
    [Inject] private IAuthService AuthService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IJSRuntime JSRuntime { get; set; }

    private UserDto currentUser;
    private string errorMessage;
    private bool isLoading = true;
    private bool showCreateWorkoutPlanModal = false;

    protected override async Task OnInitializedAsync()
    {
        await LoadUserData();
    }

    private async Task LoadUserData()
    {
        try
        {
            isLoading = true;
            currentUser = await AuthService.GetCurrentUserAsync();
            errorMessage = null;
        }
        catch (UnauthorizedAccessException)
        {
            errorMessage = "You are not authorized to access this page. Please sign in.";
            NavigateToSignIn();
        }
        catch (Exception ex)
        {
            errorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            isLoading = false;
        }
    }

    private void OpenCreateWorkoutPlanModal()
    {
        showCreateWorkoutPlanModal = true;
        StateHasChanged();
    }

    private void CloseCreateWorkoutPlanModal()
    {
        showCreateWorkoutPlanModal = false;
        StateHasChanged();
    }

    private async Task HandleWorkoutPlanCreated()
    {
        CloseCreateWorkoutPlanModal();
        await LoadUserData(); // Refresh user data after creating a workout plan
    }

    private void NavigateToSignIn()
    {
        NavigationManager.NavigateTo("/signin");
    }
}