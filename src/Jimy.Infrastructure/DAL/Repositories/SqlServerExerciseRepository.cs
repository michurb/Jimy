using Jimy.Core.Entities;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Jimy.Infrastructure.DAL.Repositories;

internal sealed class SqlServerExerciseRepository : IExerciseRepository
{
    private readonly DbSet<Exercise> _exercises;

    public SqlServerExerciseRepository(JimyDbContext dbContext)
    {
        _exercises = dbContext.Exercises;
    }
    
    public async Task<Exercise> GetByIdAsync(ExerciseId id)
        => await _exercises.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Exercise>> GetAllAsync()
        => await _exercises.ToListAsync();

    public async Task AddAsync(Exercise exercise)
        => await _exercises.AddAsync(exercise);

    public Task UpdateAsync(Exercise exercise)
    {
        _exercises.Update(exercise);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(ExerciseId id)
        => _exercises.Remove(await GetByIdAsync(id));
}