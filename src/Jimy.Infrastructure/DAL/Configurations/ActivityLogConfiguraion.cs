using Jimy.Core.Entities;
using Jimy.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimy.Infrastructure.DAL.Configurations;

internal sealed class ActivityLogConfiguraion : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.HasKey(al => al.Id);

        builder.Property(al => al.Id)
            .HasConversion(id => id.Value, value => new ActivityLogId(value));

        builder.Property(al => al.UserId)
            .HasConversion(id => id.Value, value => new UserId(value));

        builder.Property(al => al.Date)
            .HasConversion(date => date.Value, value => new Date(value));

        builder.Property(al => al.ActivityType)
            .HasConversion(type => type.Value, value => new ActivityType(value));

        builder.Property(al => al.Duration)
            .HasConversion(duration => duration.Minutes, value => new Duration(value));

        builder.Property(al => al.WorkoutPlanId)
            .HasConversion(id => id.Value, value => new WorkoutPlanId(value));

        builder.HasOne(al => al.WorkoutPlan)
            .WithMany()
            .HasForeignKey(al => al.WorkoutPlanId)
            .IsRequired(false);
    }
}