namespace Jimy.Blazor.Exceptions;

public sealed class FailedToGetUsersException : CoreException
{
  public FailedToGetUsersException() 
    : base($"Failed to get users.") {}
}