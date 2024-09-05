using Jimy.Business.Abstracition;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class DeleteWorkoutPlanHandler : ICommandHandler<DeleteWorkoutPlan>
{
    private readonly IWorkoutPlanRepository _repository;

    public DeleteWorkoutPlanHandler(IWorkoutPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(DeleteWorkoutPlan command)
    {
        await _repository.DeleteAsync(command.Id);
    }
}