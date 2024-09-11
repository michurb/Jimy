using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.WorkoutPlans;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers.WorkoutPlan;

internal sealed class GetWorkoutPlanHandler : IQueryHandler<GetWorkoutPlan, WorkoutPlanDto>
{
    private readonly JimyDbContext _dbContext;

    public GetWorkoutPlanHandler(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<WorkoutPlanDto> HandleAsync(GetWorkoutPlan query)
    {
        var workoutPlan = new WorkoutPlanId(query.WorkoutPlanId);
        var workoutPlanDto = await _dbContext.WorkoutPlans
            .AsNoTracking()
            .SingleOrDefaultAsync(wp => wp.Id == workoutPlan);
        return workoutPlanDto?.AsDto();
    }
}