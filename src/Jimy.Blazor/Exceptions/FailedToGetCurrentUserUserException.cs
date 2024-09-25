namespace Jimy.Blazor.Exceptions;

public sealed class FailedToGetCurrentUserUserException : CoreException
{
    public FailedToGetCurrentUserUserException() 
        : base($"Failed to get current user.") {}
}