namespace Jimy.Business.Exceptions;

public class UserNotFoundException : CustomException
{
    public UserNotFoundException(Guid userId)
        : base($"User with ID: '{userId}' was not found.")
    {
        UserId = userId;
    }

    public UserNotFoundException(string email)
        : base($"User with email: '{email}' was not found.")
    {
        Email = email;
    }

    public UserNotFoundException(Guid userId, string email)
        : base($"User with ID: '{userId}' and email: '{email}' was not found.")
    {
        UserId = userId;
        Email = email;
    }

    public Guid? UserId { get; }
    public string Email { get; }
}