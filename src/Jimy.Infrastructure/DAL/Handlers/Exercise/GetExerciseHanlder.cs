using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.Exercises;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers.Exercise;

internal sealed class GetExerciseHanlder : IQueryHandler<GetExercise, ExerciseDto>
{
    private readonly JimyDbContext _dbContext;

    public GetExerciseHanlder(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ExerciseDto> HandleAsync(GetExercise query)
    {
        var exerciseId = new ExerciseId(query.ExerciseId);
        var exercise = await _dbContext.Exercises
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == exerciseId);
        return exercise?.AsDto();
    }
}