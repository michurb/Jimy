using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.WorkoutPlans;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers.WorkoutPlan;

internal sealed class GetWorkoutPlansHandler : IQueryHandler<GetUsersWorkoutPlans, IEnumerable<WorkoutPlanDto>>
{
    private readonly JimyDbContext _dbContext;

    public GetWorkoutPlansHandler(JimyDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<WorkoutPlanDto>> HandleAsync(GetUsersWorkoutPlans query)
    {
        var userId = new UserId(query.UserId);
        
        var workoutPlans = await _dbContext.WorkoutPlans
            .AsNoTracking()
            .Where(wp => wp.UserId == userId)
            .Select(wp => new WorkoutPlanDto(
                wp.Id.Value,
                wp.UserId.Value,
                wp.Name.Value,
                wp.CreatedDate,
                wp.Exercises.Select(we => new WorkoutExerciseDto(
                    we.Id.Value,
                    we.ExerciseId.Value,
                    we.Exercise.Name.Value,
                    we.Sets.Value,
                    we.Reps.Value
                )).ToList()
            ))
            .ToListAsync();

        return workoutPlans;
    }
}