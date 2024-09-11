using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimy.Infrastructure.DAL.Configurations;

internal sealed class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id)
            .HasConversion(id => id.Value, value => new ExerciseId(value));

        builder.Property(e => e.Name)
            .HasConversion(name => name.Value, value => new ExerciseName(value))
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .HasConversion(desc => desc.Value, value => new ExerciseDescription(value))
            .HasMaxLength(500);
    }
}