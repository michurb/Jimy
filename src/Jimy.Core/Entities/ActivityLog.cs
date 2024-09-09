using System.Runtime.InteropServices.JavaScript;
using Jimy.Core.ValueObjects;

namespace Jimy.Core.Entities;

public class ActivityLog
{
    public ActivityLogId Id { get; private set; }
    public UserId UserId { get; private set; }
    public JSType.Date Date { get; private set; }
    public ActivityType ActivityType { get; private set; }
    public Duration Duration { get; private set; }
    public WorkoutPlanId? WorkoutPlanId { get; private set; }

    private ActivityLog() {}

    public ActivityLog(ActivityLogId id, UserId userId, JSType.Date date, ActivityType activityType, Duration duration, WorkoutPlanId? workoutPlanId = null)
    {
        Id = id;
        UserId = userId;
        Date = date;
        ActivityType = activityType;
        Duration = duration;
        WorkoutPlanId = workoutPlanId;
    }

    public void Update(JSType.Date date, ActivityType activityType, Duration duration, WorkoutPlanId? workoutPlanId)
    {
        Date = date;
        ActivityType = activityType;
        Duration = duration;
        WorkoutPlanId = workoutPlanId;
    }
}