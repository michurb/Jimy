using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimy.Infrastructure.DAL.Configurations;

internal sealed class WorkoutSessionConfiguratin : IEntityTypeConfiguration<WorkoutSession>
{
    public void Configure(EntityTypeBuilder<WorkoutSession> builder)
    {
        builder.HasKey(ws => ws.Id);
        
        builder.Property(ws => ws.Id)
            .HasConversion(id => id.Value, value => new WorkoutSessionId(value));

        builder.Property(ws => ws.UserId)
            .HasConversion(id => id.Value, value => new UserId(value));

        builder.Property(ws => ws.WorkoutPlanId)
            .HasConversion(id => id.Value, value => new WorkoutPlanId(value));

        builder.Property(ws => ws.StartTime);
        builder.Property(ws => ws.EndTime);

        builder.HasMany(ws => ws.Exercises)
            .WithOne()
            .HasForeignKey("WorkoutSessionId");

        builder.Metadata.FindNavigation(nameof(WorkoutSession.Exercises))
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}