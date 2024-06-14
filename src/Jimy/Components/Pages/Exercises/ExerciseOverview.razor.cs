using Jimmy.Shared.Domain;
using Jimy.Models;
using Microsoft.AspNetCore.Components;

namespace Jimy.Components.Pages.Exercises;

public partial class ExerciseOverview
{
    public List<Exercise> Exercises { get; set; } = default!;
    private Exercise? _selectedExercise;

    private string Title = "exercise overview";
    private string Description = "exercise overview";

    protected override void OnInitialized()
    {
        Exercises = MockDataService.Exercises;
    }


    public void ShowQuickViewPopup(Exercise selectedExercise)
    {
        _selectedExercise = selectedExercise;
    }
}