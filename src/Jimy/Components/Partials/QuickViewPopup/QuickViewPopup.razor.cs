using Jimmy.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace Jimy.Components.Partials.QuickViewPopup;

public partial class QuickViewPopup 
{
    
    [Parameter]
    public Exercise? Exercise { get; set; }
    
    private Exercise? _exercise;

    protected override void OnParametersSet()
    {
        _exercise = Exercise;
    }

    public void Close()
    {
        _exercise = null;
    }
}