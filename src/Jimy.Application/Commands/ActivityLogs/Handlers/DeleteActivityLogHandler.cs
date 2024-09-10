using Jimy.Application.Abstraction;
using Jimy.Application.Commands.Users;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.ActivityLogs.Handlers;

public sealed class DeleteActivityLogHandler(IActivityLogRepository activityLogRepository) : ICommandHandler<DeleteActivityLog>
{
    public async Task HandleAsync(DeleteActivityLog command)
    {
        var activityLogId = new ActivityLogId(command.Id);
        var activityLog = await activityLogRepository.GetByIdAsync(activityLogId);
        if (activityLog is null)
        {
            throw new ActivityLogNotFoundException(command.Id);
        }

        await activityLogRepository.DeleteAsync(activityLogId);
    }
}