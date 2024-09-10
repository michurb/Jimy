using Jimy.Application.Abstraction;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.Exercises.Handlers;

public class DeleteExerciseHandler(IExerciseRepository exerciseRepository) : ICommandHandler<DeleteExercise>
{
    public async Task HandleAsync(DeleteExercise command)
    {
        var exerciseId = new ExerciseId(command.Id);
        var exercise = await exerciseRepository.GetByIdAsync(exerciseId);
        if (exercise == null)
        {
            throw new ExerciseNotFoundException(command.Id);
        }

        await exerciseRepository.DeleteAsync(exerciseId);
    }
}