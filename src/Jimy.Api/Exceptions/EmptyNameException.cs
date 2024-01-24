namespace Jimy.Api.Exceptions;

public sealed class EmptyNameException : CustomException
{
    public EmptyNameException() : base("Exercise name already exists")
    {
    }
}