using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.Exercises;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers.Exercise;

internal sealed class GetExercisesHandler : IQueryHandler<GetExercises, IEnumerable<ExerciseDto>>
{
    private readonly JimyDbContext _dbContext;

    public GetExercisesHandler(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<ExerciseDto>> HandleAsync(GetExercises query)
    {
        var exercises = await _dbContext.Exercises
            .AsNoTracking()
            .Select(e => e.AsDto())
            .ToListAsync();
        return exercises;
    }
}