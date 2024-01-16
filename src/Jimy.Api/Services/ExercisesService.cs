using Jimy.Api.Entities;

namespace Jimy.Api.Services;

public class ExercisesService
{
    private static readonly List<Exercise> Exercises = new();

    public IEnumerable<Exercise> GetAll()
    {
        return Exercises;
    }

    public Exercise GetById(int id)
    {
        return Exercises.SingleOrDefault(x => x.Id == id);
    }

    public int? Add(Exercise exercise)
    {
        var exerciseAlreadyExists = Exercises.Any(
            x => x.Id == exercise.Id &&
            x.Name == exercise.Name);

        if (exerciseAlreadyExists)
        {
            return default;
        }
        Exercises.Add(exercise);
        return exercise.Id;
    }

    public bool Update(Exercise exercise)
    {
        var existingExercise = Exercises.SingleOrDefault(x => x.Id == exercise.Id);
        if (existingExercise is null) return false;

        existingExercise.Name = exercise.Name;
        return true;
    }

    public bool Delete(int id)
    {
        var existingExercise = Exercises.SingleOrDefault(x => x.Id == id);
        if (existingExercise is null)
        {
            return false;
        }

        Exercises.Remove(existingExercise);
        return true;
    }
}