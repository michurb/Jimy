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
    [Inject] private IWorkoutSessionService WorkoutSessionService { get; set; }
    private IEnumerable<WorkoutSessionDto> recentSessions;

    private UserDto currentUser;
    private string errorMessage;
    private bool isLoading = true;
    private bool showCreateWorkoutPlanModal = false;
    private WorkoutSessionDto activeWorkoutSession;

    protected override async Task OnInitializedAsync()
    {
        await LoadUserData();
        await CheckActiveWorkoutSession();
        await LoadRecentSessions();
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
    
    private async Task CheckActiveWorkoutSession()
    {
        try
        {
            if (currentUser != null)
            {
                activeWorkoutSession = await WorkoutSessionService.GetActiveWorkoutSessionAsync();
            }
        }
        catch (Exception ex)
        {
            errorMessage = $"Error checking active workout session: {ex.Message}";
        }
    }

    private void ResumeWorkout()
    {
        if (activeWorkoutSession != null)
        {
            NavigationManager.NavigateTo($"/active-workout/{activeWorkoutSession.Id}");
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
    
    private async Task LoadRecentSessions()
    {
        try
        {
            recentSessions = await WorkoutSessionService.GetRecentWorkoutSessionsAsync(5);
        }
        catch (Exception ex)
        {
            errorMessage = $"Error loading recent sessions: {ex.Message}";
        }
    }

    private async Task HandleWorkoutPlanCreated()
    {
        CloseCreateWorkoutPlanModal();
        await LoadUserData();
    }

    private void NavigateToSignIn()
    {
        NavigationManager.NavigateTo("/signin");
    }
}