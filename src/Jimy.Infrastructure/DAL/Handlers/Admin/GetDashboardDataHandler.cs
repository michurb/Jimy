using Azure.Core;
using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.Admin;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers.Admin;

internal sealed class GetDashboardDataHandler : IQueryHandler<GetDashboardData, DashboardDataDto>
{
    private readonly JimyDbContext _dbContext;

    public GetDashboardDataHandler(JimyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<DashboardDataDto> HandleAsync(GetDashboardData query)
    {
        var totalUsers = await _dbContext.Users.CountAsync();
        var totalWorkouts = await _dbContext.WorkoutPlans.CountAsync();
        var totalExercises = await _dbContext.Exercises.CountAsync();

        var dashboardData = new DashboardDataDto(totalUsers,totalWorkouts,totalExercises);

        return dashboardData;
    }
}
