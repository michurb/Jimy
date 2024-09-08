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
    public DbSet<WorkoutSession> WorkoutSessions { get; set; }
    public DbSet<WorkoutSessionExercise> WorkoutSessionExercises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Username).IsUnique();
            entity.Property(u => u.Username).HasMaxLength(50);
            entity.Property(u => u.Email).HasMaxLength(100);
        });

        // Configure WorkoutPlan entity
        modelBuilder.Entity<WorkoutPlan>(entity =>
        {
            entity.HasOne(wp => wp.User)
                .WithMany()
                .HasForeignKey(wp => wp.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Exercise entity
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Configure WorkoutExercise entity
        modelBuilder.Entity<WorkoutExercise>(entity =>
        {
            entity.HasOne(we => we.WorkoutPlan)
                .WithMany(wp => wp.Exercises)
                .HasForeignKey(we => we.WorkoutPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(we => we.Exercise)
                .WithMany()
                .HasForeignKey(we => we.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure ActivityLog entity
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(al => al.UserId);
            entity.HasOne<WorkoutPlan>()
                .WithMany()
                .HasForeignKey(al => al.WorkoutPlanId);
            entity.Property(al => al.ActivityType).HasMaxLength(50);
        });
        
        //Configure WorkoutSessionExercise entity
        modelBuilder.Entity<WorkoutSessionExercise>(entity =>
        {
            entity.HasOne(wse => wse.Exercise)
                .WithMany()
                .HasForeignKey(wse => wse.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.Weight).HasColumnType("decimal(5,2)");
        });
    }
}