﻿using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.WorkoutPlans;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers.WorkoutSession;

internal sealed class GetRecentWorkoutSessionsHandler : IQueryHandler<GetRecentWorkoutSessions, IEnumerable<WorkoutSessionDto>>
{
    private readonly JimyDbContext _dbContext;

    public GetRecentWorkoutSessionsHandler(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WorkoutSessionDto>> HandleAsync(GetRecentWorkoutSessions query)
    {
        var userId = new UserId(query.UserId);

        var recentSessions = await _dbContext.WorkoutSessions
            .AsNoTracking()
            .Where(ws => ws.UserId == userId && ws.EndTime != null)
            .OrderByDescending(ws => ws.EndTime)
            .Take(query.Count)
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
            .ToListAsync();

        return recentSessions;
    }
}