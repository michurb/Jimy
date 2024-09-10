namespace Jimy.Core.Exceptions;

public sealed class ActivityLogNotFoundException : CoreException
{
    public ActivityLogNotFoundException(Guid activityLogId) 
        : base($"Exercise with ID {activityLogId} was not found.") {}
}