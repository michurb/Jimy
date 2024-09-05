namespace Jimy.Business.Exceptions;

public class ActivityLogNotFoundException : CustomException
{
    public ActivityLogNotFoundException(int activityLogId) : base(
        $"Activity log with ID: '{activityLogId}' was not found.")
    {
        ActivityLogId = activityLogId;
    }

    public int ActivityLogId { get; }
}