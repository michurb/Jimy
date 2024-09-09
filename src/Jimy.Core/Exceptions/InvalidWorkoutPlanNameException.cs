namespace Jimy.Core.Exceptions;

public sealed class InvalidWorkoutPlanNameException : CoreException
{
    public string Name { get; }

    public InvalidWorkoutPlanNameException(string name) : base($"Workout name: {name} is invalid.")
    {
        Name = name;
    }
}