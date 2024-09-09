namespace Jimy.Core.Exceptions;

public class ExerciseNotFoundException : CoreException
{
    public ExerciseNotFoundException(int exerciseId) 
        : base($"Exercise with ID {exerciseId} was not found.") {}
}