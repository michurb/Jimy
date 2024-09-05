using Jimy.Business.Abstracition;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class DeleteActivityLogHandler : ICommandHandler<DeleteActivityLog>
{
    private readonly IActivityLogRepository _repository;

    public DeleteActivityLogHandler(IActivityLogRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(DeleteActivityLog command)
    {
        await _repository.DeleteAsync(command.Id);
    }
}