namespace Jimy.Core.Exceptions;

public sealed class InvalidUsernameException : CoreException
{
    public string UserName {get;}

    public InvalidUsernameException(string userName) : base($"Username: {userName} id invalid.")
    {
        UserName = userName;
    }
}