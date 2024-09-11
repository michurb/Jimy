using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.WorkoutPlans;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Jimy.Infrastructure.DAL.Handlers.WorkoutSession;

internal sealed class GetWorkoutSessionHandler : IQueryHandler<GetWorkoutSession, WorkoutSessionDto>
{
    private readonly JimyDbContext _dbContext;

    public GetWorkoutSessionHandler(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<WorkoutSessionDto> HandleAsync(GetWorkoutSession query)
    {
        var workoutSessionId = new WorkoutSessionId(query.WorkoutSessionId);
        var workoutSession = await _dbContext.WorkoutSessions
            .AsNoTracking()
            .SingleOrDefaultAsync(ws => ws.Id == workoutSessionId);
        return workoutSession?.AsDto();
    }
}