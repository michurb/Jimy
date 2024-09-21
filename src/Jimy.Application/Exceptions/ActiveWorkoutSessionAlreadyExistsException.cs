using Jimy.Core.Exceptions;

namespace Jimy.Application.Exceptions;

public sealed class ActiveWorkoutSessionAlreadyExistsException : CoreException
{
    public ActiveWorkoutSessionAlreadyExistsException() : base("You have an active workout session.")
    {
        
    }
}