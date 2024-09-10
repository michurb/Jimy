using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;

namespace Jimy.Core.Interfaces;

public interface IExerciseRepository
{
    Task<Exercise> GetByIdAsync(ExerciseId id);
    Task<IEnumerable<Exercise>> GetAllAsync();
    Task AddAsync(Exercise exercise);
    Task UpdateAsync(Exercise exercise);
    Task DeleteAsync(ExerciseId id);
}