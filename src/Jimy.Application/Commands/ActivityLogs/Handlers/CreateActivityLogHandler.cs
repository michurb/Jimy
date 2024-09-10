using Jimy.Application.Abstraction;
using Jimy.Application.Commands.WorkoutPlans;
using Jimy.Core.Entities;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.ActivityLogs.Handlers;

public sealed class CreateActivityLogHandler(IActivityLogRepository activityLogRepository,
    IWorkoutPlanRepository workoutPlanRepository) : ICommandHandler<CreateActivityLog>
{
    public async Task HandleAsync(CreateActivityLog command)
    {
        var userId = new UserId(command.UserId);
        var activityType = new ActivityType(command.ActivityType);
        var duration = new Duration(command.Duration);

        WorkoutPlanId? workoutPlanId = null;
        if (command.WorkoutPlanId.HasValue)
        {
            workoutPlanId = new WorkoutPlanId(command.WorkoutPlanId.Value);
            var workoutPlan = await workoutPlanRepository.GetByIdAsync(workoutPlanId);
            if (workoutPlan == null)
            {
                throw new WorkoutPlanNotFoundException(command.WorkoutPlanId.Value);
            }
        }

        var activityLog = new ActivityLog(
            ActivityLogId.NewId(),
            userId,
            new Date(command.Date),
            activityType,
            duration,
            workoutPlanId
        );

        await activityLogRepository.AddAsync(activityLog);
    }
}