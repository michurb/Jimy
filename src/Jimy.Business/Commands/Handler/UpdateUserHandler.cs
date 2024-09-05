using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.Exceptions;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class UpdateUserHandler : ICommandHandler<UpdateUser>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;

    public UpdateUserHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task HandleAsync(UpdateUser command)
    {
        var user = await _repository.GetByIdAsync(command.Id);
        if (user == null)
            throw new UserNotFoundException(command.Id);

        _mapper.Map(command.Dto, user);
        await _repository.UpdateAsync(user);
    }
}