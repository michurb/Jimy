using Jimy.Application.Abstraction;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.Exercises.Handlers;

public class UpdateExerciseHandler(IExerciseRepository exerciseRepository) : ICommandHandler<UpdateExercise>
{
    public async Task HandleAsync(UpdateExercise command)
    {
        var exerciseId = new ExerciseId(command.Id);
        var exercise = await exerciseRepository.GetByIdAsync(exerciseId);
        if (exercise == null)
        {
            throw new ExerciseNotFoundException(command.Id);
        }

        exercise.UpdateDetails(
            new ExerciseName(command.Name),
            new ExerciseDescription(command.Description)
        );

        await exerciseRepository.UpdateAsync(exercise);
    }
}