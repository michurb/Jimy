namespace Jimy.Api.Exceptions;

public abstract class CustomException : Exception
{
    protected CustomException(string message): base(message)
    {

    }
}