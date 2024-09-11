using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimy.Infrastructure.DAL.Configurations;

internal sealed class WorkoutSessionExerciseConfiguration : IEntityTypeConfiguration<WorkoutSessionExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutSessionExercise> builder)
    {
        builder.HasKey(wse => new { wse.WorkoutSessionId, wse.ExerciseId });
        
        builder.Property(wse => wse.WorkoutSessionId)
            .HasConversion(id => id.Value, value => new WorkoutSessionId(value));

        builder.Property(wse => wse.ExerciseId)
            .HasConversion(id => id.Value, value => new ExerciseId(value));

        builder.Property(wse => wse.Sets)
            .HasConversion(sets => sets.Value, value => new Sets(value));

        builder.Property(wse => wse.Reps)
            .HasConversion(reps => reps.Value, value => new Reps(value));

        builder.Property(wse => wse.Weight)
            .HasConversion(weight => weight.Value, value => new Weight(value));

        builder.HasOne(wse => wse.Exercise)
            .WithMany()
            .HasForeignKey(wse => wse.ExerciseId);
    }
}