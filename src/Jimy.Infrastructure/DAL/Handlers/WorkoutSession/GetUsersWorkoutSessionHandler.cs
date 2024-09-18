using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.WorkoutPlans;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers.WorkoutSession;

internal sealed class GetUsersWorkoutSessionHandler : IQueryHandler<GetUsersWorkoutSession, IEnumerable<WorkoutSessionDto>> 
{
    private readonly JimyDbContext _dbContext;

    public GetUsersWorkoutSessionHandler(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<WorkoutSessionDto>> HandleAsync(GetUsersWorkoutSession query)
    {
        var userId = new UserId(query.UserId);

        var workoutSessions = await _dbContext.WorkoutSessions
            .AsNoTracking()
            .Where(ws => ws.UserId == userId)
            .Include(ws => ws.Exercises)
            .ThenInclude(wse => wse.Exercise) 
            .OrderByDescending(ws => ws.StartTime)
            .Select(ws => new WorkoutSessionDto(
                ws.Id.Value,
                ws.UserId.Value,
                ws.WorkoutPlanId.Value,
                ws.StartTime,
                ws.EndTime,
                ws.Exercises.Select(e => new WorkoutSessionExerciseDto(
                    e.ExerciseId.Value,
                    e.Exercise.Id.Value,
                    e.Exercise.Name.Value,
                    e.Sets.Value,
                    e.Reps.Value,
                    e.Weight.Value
                )).ToList()
            ))
            .ToListAsync();

        return workoutSessions;
    }
}