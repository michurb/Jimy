using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimy.Infrastructure.DAL.Configurations;

internal sealed class WorkoutExerciseConfiguration : IEntityTypeConfiguration<WorkoutExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
    {
        builder.HasKey(we => we.Id);

        builder.Property(we => we.Id)
            .HasConversion(id => id.Value, value => new WorkoutExerciseId(value));

        builder.Property(we => we.Sets)
            .HasConversion(sets => sets.Value, value => new Sets(value));

        builder.Property(we => we.Reps)
            .HasConversion(reps => reps.Value, value => new Reps(value));

        builder.HasOne(we => we.Exercise)
            .WithMany()
            .HasForeignKey(we => we.ExerciseId);
    }
}