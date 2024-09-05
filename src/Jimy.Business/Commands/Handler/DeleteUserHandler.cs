using Jimy.Business.Abstracition;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class DeleteUserHandler : ICommandHandler<DeleteUser>
{
    private readonly IUserRepository _repository;

    public DeleteUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(DeleteUser command)
    {
        await _repository.DeleteAsync(command.Id);
    }
}