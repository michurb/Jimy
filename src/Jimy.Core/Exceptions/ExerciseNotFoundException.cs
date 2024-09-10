namespace Jimy.Core.Exceptions;

public sealed class ExerciseNotFoundException : CoreException
{
    public ExerciseNotFoundException(Guid exerciseId) 
        : base($"Exercise with ID {exerciseId} was not found.") {}
}