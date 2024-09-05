namespace Jimy.Business.Exceptions;

public class EmailAlreadyInUseException : CustomException
{
    public EmailAlreadyInUseException(string email) : base($"Email '{email}' is already in use.")
    {
        Email = email;
    }

    public string Email { get; }
}