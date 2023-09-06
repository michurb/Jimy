using System.Data.Common;
using Jimy.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Api.Data;

public class JimyDbContext : DbContext
{
    public JimyDbContext(DbContextOptions<JimyDbContext> options) : base(options)
    {
    }

    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<TrainingSession> TrainingSessions { get; set; }
    public DbSet<ExerciseDetails> ExerciseDetails { get; set; }
}