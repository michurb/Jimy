namespace Jimy.Core.Exceptions;

public sealed class InvalidExerciseDescriptionException : CoreException
{
    public InvalidExerciseDescriptionException() : base("Description is invalid.") { }
}