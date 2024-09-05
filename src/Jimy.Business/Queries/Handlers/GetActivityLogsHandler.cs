using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Queries.Handlers;

public class GetActivityLogsHandler : IQueryHandler<GetActivityLogs, IEnumerable<ActivityLogDto>>
{
    private readonly IMapper _mapper;
    private readonly IActivityLogRepository _repository;

    public GetActivityLogsHandler(IActivityLogRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ActivityLogDto>> HandleAsync(GetActivityLogs query)
    {
        var activityLogs = await _repository.GetByUserIdAsync(query.UserId);
        return _mapper.Map<IEnumerable<ActivityLogDto>>(activityLogs);
    }
}