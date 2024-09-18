using Jimy.Core.ValueObjects;

namespace Jimy.Core.Entities;

public class WorkoutSet
{
    public Sets SetNumber { get; private set; }
    public Weight Weight { get; private set; }

    private WorkoutSet() { }

    public WorkoutSet(Sets setNumber, Weight weight)
    {
        SetNumber = setNumber;
        Weight = weight;
    }

    public void UpdateWeight(Weight weight)
    {
        Weight = weight;
    }
}