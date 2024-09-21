using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.WorkoutPlans;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers.WorkoutSession;

internal sealed class GetUserActiveWorkoutSessionHandler : IQueryHandler<GetUserActiveWorkoutSession, WorkoutSessionDto>
{
    private readonly JimyDbContext _dbContext;

    public GetUserActiveWorkoutSessionHandler(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<WorkoutSessionDto> HandleAsync(GetUserActiveWorkoutSession query)
    {
        var userId = new UserId(query.UserId);
        
        var workoutSession = await _dbContext.WorkoutSessions
            .AsNoTracking()
            .Where(ws => ws.UserId == userId && ws.EndTime == null)
            .Include(ws => ws.Exercises)
            .ThenInclude(wse => wse.Exercise)
            .Include(ws => ws.Exercises)
            .ThenInclude(wse => wse.SetDetails)
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
                    e.SetDetails.Select(s => new WorkoutSetDto(
                        s.SetNumber.Value,
                        s.Weight.Value
                    ))
                )).ToList()
            ))
            .FirstOrDefaultAsync();

        return workoutSession;
    }
}