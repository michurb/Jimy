using Jimy.Core.Interfaces;
using Jimy.Infrastructure.DAL;
using Jimy.Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jimy.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "sqlserver";
    
    public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SqlServerOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var sqlServerOptions = configuration.GetOptions<SqlServerOptions>(OptionsSectionName);
        services.AddDbContext<JimyDbContext>(x => x.UseSqlServer(sqlServerOptions.ConnectionString));
        services.AddScoped<IUserRepository, SqlServerUserRepository>();
        services.AddScoped<IWorkoutPlanRepository, SqlServerWorkoutPlanRepository>();
        services.AddScoped<IExerciseRepository, SqlServerExerciseRepository>();
        services.AddScoped<IActivityLogRepository, SqlServerActivityLogRepository>();
        services.AddScoped<IWorkoutSessionRepository, SqlServerWorkoutSessionRepository>();
        
        return services;
    }
}