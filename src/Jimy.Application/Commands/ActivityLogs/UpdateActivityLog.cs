using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.ActivityLogs;

public record UpdateActivityLog(Guid Id, DateTime Date, string ActivityType, int Duration, Guid? WorkoutPlanId) : ICommand;