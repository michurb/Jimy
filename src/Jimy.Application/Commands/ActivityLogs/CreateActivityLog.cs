using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.ActivityLogs;

public record CreateActivityLog(Guid UserId, DateTime Date, string ActivityType, int Duration, Guid? WorkoutPlanId) : ICommand;