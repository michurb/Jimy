namespace Jimy.Core.Exceptions;

public sealed class ExerciseNotFoundException : CoreException
{
    public ExerciseNotFoundException(int exerciseId) 
        : base($"Exercise with ID {exerciseId} was not found.") {}
}