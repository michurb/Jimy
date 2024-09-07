namespace Jimy.Business.Exceptions;

public class ExerciseAlreadyExistsException : CustomException
{
    public ExerciseAlreadyExistsException(string name) 
        : base($"An exercise with the name '{name}' already exists.")
    {
        Name = name;
    }

    public string Name { get; }
}