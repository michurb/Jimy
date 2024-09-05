using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;
using Jimy.Business.Exceptions;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Queries.Handlers;

public class GetUserByEmailHandler : IQueryHandler<GetUserByEmail, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;

    public GetUserByEmailHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDto> HandleAsync(GetUserByEmail query)
    {
        var user = await _repository.GetByEmailAsync(query.Email);
        if (user == null)
            throw new UserNotFoundException(query.Email);

        return _mapper.Map<UserDto>(user);
    }
}