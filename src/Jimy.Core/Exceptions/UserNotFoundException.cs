﻿namespace Jimy.Core.Exceptions;

public sealed class UserNotFoundException : CoreException
{
    public UserNotFoundException(Guid userId) 
        : base($"User with ID {userId} was not found.") {}

    public UserNotFoundException(string email) 
        : base($"User with email {email} was not found.") {}
}