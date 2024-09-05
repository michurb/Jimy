namespace Jimy.Business.Exceptions;

public class ExerciseNotFoundException : CustomException
{
    public ExerciseNotFoundException(int exerciseId) : base($"Exercise with ID: '{exerciseId}' was not found.")
    {
        ExerciseId = exerciseId;
    }

    public int ExerciseId { get; }
}