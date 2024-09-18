namespace Jimy.Core.Exceptions;

public sealed class WorkoutExerciseNotFoundException : CoreException
{
    public WorkoutExerciseNotFoundException (Guid exerciseId) 
        : base($"WorkoutSession with ID {exerciseId} was not found.") {}
}