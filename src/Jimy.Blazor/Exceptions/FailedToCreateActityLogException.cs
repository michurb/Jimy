namespace Jimy.Blazor.Exceptions;

public sealed class FailedToCreateActityLogException : CoreException
{
    public FailedToCreateActityLogException() 
        : base($"Failed to create activitylog.") {}
}