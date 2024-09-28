namespace Jimy.Blazor.Exceptions;

public sealed class UserNotFoundException : CoreException
{
  public UserNotFoundException(Guid userId) 
    : base($"User with id: {userId} not found.") {}
}