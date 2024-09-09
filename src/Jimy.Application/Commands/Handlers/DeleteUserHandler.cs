using Jimy.Application.Abstraction;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;

namespace Jimy.Application.Commands.Handlers;

public class DeleteUsersHandler : ICommandHandler<DeleteUser, Guid>
{
    private readonly IUserRepository _repository;

    public DeleteUsersHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(DeleteUser command)
    {
            var user = await _repository.GetByIdAsync(command.Id);
            if (user != null)
            {
                await _repository.DeleteAsync(user.Id);
                return command.Id;
            }

            throw new UserNotFoundException(command.Id);
    }
}