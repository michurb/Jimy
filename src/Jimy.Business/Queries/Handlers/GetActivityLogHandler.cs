using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;
using Jimy.Business.Exceptions;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Queries.Handlers;

public class GetActivityLogHandler : IQueryHandler<GetActivityLog, ActivityLogDto>
{
    private readonly IMapper _mapper;
    private readonly IActivityLogRepository _repository;

    public GetActivityLogHandler(IActivityLogRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ActivityLogDto> HandleAsync(GetActivityLog query)
    {
        var activityLog = await _repository.GetByIdAsync(query.Id);
        if (activityLog == null)
            throw new ActivityLogNotFoundException(query.Id);

        return _mapper.Map<ActivityLogDto>(activityLog);
    }
}