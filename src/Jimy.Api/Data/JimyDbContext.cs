using System.Data.Common;
using Jimy.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Api.Data;

public class JimyDbContext : DbContext
{
    public JimyDbContext(DbContextOptions<JimyDbContext> options) : base(options)
    {
    }

    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();
    public DbSet<ExerciseDetails> ExerciseDetails => Set<ExerciseDetails>();
}