using Jimy.Application.Abstraction;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.Users.Handlers;

public sealed class UpdateUserHandler(IUserRepository userRepository) : ICommandHandler<UpdateUser>
{
    public async Task HandleAsync(UpdateUser command)
    {
        var user = await userRepository.GetByIdAsync(new UserId(command.Id));
        if (user is null)
        {
            throw new UserNotFoundException(command.Id);
        }

        var role = new Role(command.Role);
        user.UpdateDetails(new Username(command.Username), new Email(command.Email), role);
        await userRepository.UpdateAsync(user);
    }
}
