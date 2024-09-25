namespace Jimy.Blazor.Exceptions;

public sealed class RegistrationFailedException : CoreException
{
    public RegistrationFailedException() 
        : base($"Registration fail.") {}
}