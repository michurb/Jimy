using Jimy.Application.Abstraction;

namespace Jimy.Application.Commands.ActivityLogs;

public record DeleteActivityLog(Guid Id) : ICommand;