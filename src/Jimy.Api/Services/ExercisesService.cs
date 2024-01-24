using Jimy.Api.Commands;
using Jimy.Api.DTO;
using Jimy.Api.Entities;

namespace Jimy.Api.Services;

public class ExercisesService
{
    private static readonly List<Exercise> Exercises = new();

    public IEnumerable<ExerciseDto> GetAll() => Exercises.Select(x => new ExerciseDto
    {
        Id = x.Id,
        Name = x.Name
    });

    public ExerciseDto GetById(Guid id) => GetAll().SingleOrDefault(x => x.Id == id);

    public Guid? Add(CreateExercise command)
    {
        var exerciseAlreadyExists = Exercises.Any(
            x => x.Id == command.Id &&
            x.Name == command.Name);

        if (exerciseAlreadyExists)
        {
            return default;
        }
        var exercise = new Exercise(command.Id, command.Name);
        Exercises.Add(exercise);
        return command.Id;
    }

    public bool Update(ChangeExerciseName command)
    {
        var existingExercise = Exercises.SingleOrDefault(x => x.Id == command.ExerciseId);
        if (existingExercise is null)
        {
            return false;
        }

        existingExercise.UpdateName(command.Name);
        return true;
    }
    

    public bool Delete(DeleteExercise command)
    {
        var existingExercise = Exercises.SingleOrDefault(x => x.Id == command.ExerciseId);
        if (existingExercise is null)
        {
            return false;
        }

        Exercises.Remove(existingExercise);
        return true;
    }
    
}