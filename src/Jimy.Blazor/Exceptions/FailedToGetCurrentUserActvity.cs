namespace Jimy.Blazor.Exceptions;

public sealed class FailedToGetCurrentUserActvity : CoreException
{
    public FailedToGetCurrentUserActvity() 
        : base($"Failed to get activitylogs.") {}
}