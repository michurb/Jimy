using Jimy.Application.Abstraction;
using Jimy.Core.Entities;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.Users.Handlers;

public sealed class CreateUserHandler(IUserRepository userRepository) : ICommandHandler<CreateUser>
{
    public async Task HandleAsync(CreateUser command)
    {
        var user = new User(UserId.NewId(),
            new Email(command.Email),
            new Username(command.Username),
            new Password(command.Password),
            Role.User(),
            DateTime.UtcNow);
        await userRepository.AddAsync(user);
    }
}