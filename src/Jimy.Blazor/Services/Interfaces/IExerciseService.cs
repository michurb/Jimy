using Jimy.Blazor.Models;

namespace Jimy.Blazor.Services.Interfaces;

public interface IExerciseService
{
    Task<IEnumerable<ExerciseDto>> GetExercisesAsync();
    Task CreateExerciseAsync(CreateExerciseDto createExerciseDto);
    Task UpdateExerciseAsync(EditExerciseDto editExerciseDto);
    Task DeleteExerciseAsync(Guid exerciseId);
}