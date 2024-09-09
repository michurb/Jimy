namespace Jimy.Core.Exceptions;

public sealed class InvalidExerciseIdException : CoreException
{
    public InvalidExerciseIdException() : base("Invalid exercise ID.") { }
}