using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.Exceptions;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class UpdateActivityLogHandler : ICommandHandler<UpdateActivityLog>
{
    private readonly IMapper _mapper;
    private readonly IActivityLogRepository _repository;

    public UpdateActivityLogHandler(IActivityLogRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task HandleAsync(UpdateActivityLog command)
    {
        var activityLog = await _repository.GetByIdAsync(command.Id);
        if (activityLog == null)
            throw new ActivityLogNotFoundException(command.Id);

        _mapper.Map(command.Dto, activityLog);
        await _repository.UpdateAsync(activityLog);
    }
}