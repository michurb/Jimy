using Jimy.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL;

internal sealed class JimyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<WorkoutSession> WorkoutSessions { get; set; }

    public JimyDbContext(DbContextOptions<JimyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}