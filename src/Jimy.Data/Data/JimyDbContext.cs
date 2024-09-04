using Jimy.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Data.Data;

public class JimyDbContext : DbContext
{
    public JimyDbContext(DbContextOptions<JimyDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure entity relationships and constraints here
        modelBuilder.Entity<WorkoutPlan>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(wp => wp.UserId);

        modelBuilder.Entity<WorkoutExercise>()
            .HasOne<WorkoutPlan>()
            .WithMany()
            .HasForeignKey(we => we.WorkoutPlanId);

        modelBuilder.Entity<WorkoutExercise>()
            .HasOne<Exercise>()
            .WithMany()
            .HasForeignKey(we => we.ExerciseId);

        modelBuilder.Entity<ActivityLog>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(al => al.UserId);

        modelBuilder.Entity<ActivityLog>()
            .HasOne<WorkoutPlan>()
            .WithMany()
            .HasForeignKey(al => al.WorkoutPlanId);
    }
}