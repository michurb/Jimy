using Jimmy.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace Jimy.Components.Partials.ExerciseCard;

public partial class ExerciseCard 
{
    [Parameter]
    public Exercise Exercise { get; set; } = default!;
    
    [Parameter]
    public EventCallback<Exercise> ExerciseQuickViewClicked { get; set; }
    
}