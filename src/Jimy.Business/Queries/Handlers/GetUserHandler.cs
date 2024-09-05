using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;
using Jimy.Business.Exceptions;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Queries.Handlers;

public class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;

    public GetUserHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDto> HandleAsync(GetUser query)
    {
        var user = await _repository.GetByIdAsync(query.Id);
        if (user == null)
            throw new UserNotFoundException(query.Id);

        return _mapper.Map<UserDto>(user);
    }
}