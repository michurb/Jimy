using Jimy.Core.Entities;

namespace Jimy.Core.Interfaces;

public interface IExerciseRepository
{
    Task<Exercise> GetByIdAsync(int id);
    Task<IEnumerable<Exercise>> GetAllAsync();
    Task AddAsync(Exercise exercise);
    Task UpdateAsync(Exercise exercise);
    Task DeleteAsync(int id);
}