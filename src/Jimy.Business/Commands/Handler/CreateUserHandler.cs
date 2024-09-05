using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class CreateUserHandler : ICommandHandler<CreateUser>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;

    public CreateUserHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task HandleAsync(CreateUser command)
    {
        var user = _mapper.Map<User>(command.Dto);
        await _repository.AddAsync(user);
    }
}