using Jimy.Application.Abstraction;
using Jimy.Core.Entities;
using Jimy.Core.Interfaces;

namespace Jimy.Application.Commands.Handlers;

public class CreateUserHandler : ICommandHandler<CreateUser, Guid>
{
    private readonly IUserRepository _repository;

    public CreateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateUser command)
    {
        var user = new User(command.Username, command.Email);
        await _repository.AddAsync(user);
        await _repository.UpdateAsync(user);
        return user.Id;
    }
}