using Jimmy.Shared.Domain;
using Jimy.Models;
using Microsoft.AspNetCore.Components;

namespace Jimy.Components.Pages.Exercises;

public partial class ExerciseOverview : ComponentBase
{
    public List<Exercise> Exercises { get; set; } = default!;
    private Exercise? _selectedExercise;

    protected override void OnInitialized()
    {
        Exercises = new List<Exercise>
        {
            new Exercise { Name = "Exercise 1", Description = "Description 1" },
            new Exercise { Name = "Exercise 2", Description = "Description 2" },
            new Exercise { Name = "Exercise 3", Description = "Description 3" },
        };
    }
    
    public void ShowQuickViewPopup(Exercise selectedExercise)
    {
        _selectedExercise = selectedExercise;
        Console.WriteLine($"Selected Exercise: {_selectedExercise?.Name}");
        StateHasChanged();
    }
}