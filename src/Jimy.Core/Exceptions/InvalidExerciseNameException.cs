namespace Jimy.Core.Exceptions;

public sealed class InvalidExerciseNameException : CoreException
{
    public InvalidExerciseNameException() : base("Invalid exercise name") { }
}