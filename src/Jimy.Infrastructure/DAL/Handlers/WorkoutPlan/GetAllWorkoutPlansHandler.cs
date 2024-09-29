using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.WorkoutPlans;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers.WorkoutPlan;

internal sealed class GetAllWorkoutPlansHandler : IQueryHandler<GetAllWorkoutPlans ,IEnumerable<WorkoutPlanDto>>
{
    private readonly JimyDbContext _dbContext;

    public GetAllWorkoutPlansHandler(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<WorkoutPlanDto>> HandleAsync(GetAllWorkoutPlans query)
    {
        return  await _dbContext.WorkoutPlans
            .AsNoTracking()
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
    }
}
