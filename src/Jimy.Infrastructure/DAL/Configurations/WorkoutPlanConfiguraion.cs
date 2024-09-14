using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimy.Infrastructure.DAL.Configurations;

internal sealed class WorkoutPlanConfiguraion : IEntityTypeConfiguration<WorkoutPlan>
{
    public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
    {
        builder.HasKey(wp => wp.Id);
        builder.Property(wp => wp.Id)
            .HasConversion(id => id.Value, value => new WorkoutPlanId(value));

        builder.Property(wp => wp.UserId)
            .HasConversion(id => id.Value, value => new UserId(value));

        builder.Property(wp => wp.Name)
            .HasConversion(name => name.Value, value => new WorkoutPlanName(value));

        builder.HasMany(wp => wp.Exercises)
            .WithOne()
            .HasForeignKey("WorkoutPlanId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.FindNavigation(nameof(WorkoutPlan.Exercises))
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}