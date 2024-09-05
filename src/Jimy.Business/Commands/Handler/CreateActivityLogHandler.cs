using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class CreateActivityLogHandler : ICommandHandler<CreateActivityLog>
{
    private readonly IMapper _mapper;
    private readonly IActivityLogRepository _repository;

    public CreateActivityLogHandler(IActivityLogRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task HandleAsync(CreateActivityLog command)
    {
        var activityLog = _mapper.Map<ActivityLog>(command.Dto);
        await _repository.AddAsync(activityLog);
    }
}