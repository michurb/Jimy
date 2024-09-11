using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimy.Infrastructure.DAL.Configurations;

internal sealed class WorkoutExerciseConfiguration : IEntityTypeConfiguration<WorkoutExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
    {
        builder.HasKey(we => new { we.ExerciseId });

        builder.Property(we => we.ExerciseId)
            .HasConversion(id => id.Value, value => new ExerciseId(value));

        builder.Property(we => we.Sets)
            .HasConversion(sets => sets.Value, value => new Sets(value));

        builder.Property(we => we.Reps)
            .HasConversion(reps => reps.Value, value => new Reps(value));

        builder.HasOne(we => we.Exercise)
            .WithMany()
            .HasForeignKey(we => we.ExerciseId);
    }
}