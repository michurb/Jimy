using Jimy.Application.Abstraction;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.Users.Handlers;

public sealed class DeleteUserHandler(IUserRepository userRepository) : ICommandHandler<DeleteUser>
{
    public async Task HandleAsync(DeleteUser command)
    {
        var userId = new UserId(command.Id);
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            throw new UserNotFoundException(command.Id);
        }
        await userRepository.DeleteAsync(userId);
        await userRepository.UpdateAsync(user);
    }
}