using Jimmy.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace Jimy.Components.Partials.ExerciseCard;

public partial class ExerciseCard 
{
    [Parameter]
    public Exercise Exercise { get; set; } = default!;

    [Parameter]
    public EventCallback<Exercise> ExerciseQuickViewClicked { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        if (string.IsNullOrEmpty(Exercise.Name))
        {
            throw new Exception("Last name can't be empty");
        }
    }

    public void NavigateToDetails(Exercise selectedExercise)
    {

        NavigationManager.NavigateTo($"/exercisedetail/{selectedExercise.Id}");

    }
    
}