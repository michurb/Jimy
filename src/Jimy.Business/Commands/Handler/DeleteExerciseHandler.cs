using Jimy.Business.Abstracition;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class DeleteExerciseHandler : ICommandHandler<DeleteExercise>
{
    private readonly IExerciseRepository _repository;

    public DeleteExerciseHandler(IExerciseRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(DeleteExercise command)
    {
        await _repository.DeleteAsync(command.Id);
    }
}