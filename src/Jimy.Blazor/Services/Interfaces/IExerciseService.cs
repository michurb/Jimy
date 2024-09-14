using Jimy.Blazor.Models;

namespace Jimy.Blazor.API.Interfaces;

public interface IExerciseService
{
    Task<List<ExerciseDto>> GetExercisesAsync();
}