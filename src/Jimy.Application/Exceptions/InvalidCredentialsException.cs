using Jimy.Core.Exceptions;

namespace Jimy.Application.Exceptions;

public sealed class InvalidCredentialsException : CoreException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}