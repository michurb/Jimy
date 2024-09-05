namespace Jimy.Business.Exceptions;

public class UsernameAlreadyInUseException : CustomException
{
    public UsernameAlreadyInUseException(string username) : base($"Username '{username}' is already in use.")
    {
        Username = username;
    }

    public string Username { get; }
}