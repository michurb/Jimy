namespace Jimy.Core.Exceptions;

public sealed class InvalidWorkoutPlanIdException : CoreException
{
    public InvalidWorkoutPlanIdException() : base("Invalid workout plan ID.") { }
}