using Jimy.Application.Abstraction;
using Jimy.Application.Commands.WorkoutPlans;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.ActivityLogs.Handlers;

public sealed class UpdateActivityLogHandler(IActivityLogRepository activityLogRepository,
    IWorkoutPlanRepository workoutPlanRepository) : ICommandHandler<UpdateActivityLog>
{
    public async Task HandleAsync(UpdateActivityLog command)
    {
        var activityLogId = new ActivityLogId(command.Id);
        var activityLog = await activityLogRepository.GetByIdAsync(activityLogId);
        if (activityLog == null)
        {
            throw new ActivityLogNotFoundException(command.Id);
        }

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

        activityLog.Update(new Date(command.Date), activityType, duration, workoutPlanId);
        await activityLogRepository.UpdateAsync(activityLog);
    }
}