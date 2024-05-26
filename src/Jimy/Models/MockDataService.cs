

using Jimmy.Shared.Domain;

namespace Jimy.Models;

public class MockDataService
{
    private static List<Exercise>? _exercises = default!;

    public static List<Exercise> Exercises
    {
        get => _exercises ??= InitializeExercises();
    }

    private static List<Exercise>? InitializeExercises()
    {
        var e1 = new Exercise
        {
            Id = 1,
            Name = "Pushups",
            Description = "Pushups are a great exercise for the upper body."
        };
        
        var e2 = new Exercise
        {
            Id = 2,
            Name = "Pullups",
            Description = "Pullups are a great exercise for the upper body."
        };
        return new List<Exercise>() { e1, e2 };
    }
}