using Jimy.Application.Abstraction;
using Jimy.Core.Entities;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.Exercises.Handlers;

public class CreateExerciseHandler(IExerciseRepository exerciseRepository) : ICommandHandler<CreateExercise>
{
    public async Task HandleAsync(CreateExercise command)
    {
        var exercise = new Exercise(
            ExerciseId.NewId(),
            new ExerciseName(command.Name),
            new ExerciseDescription(command.Description)
        );

        await exerciseRepository.AddAsync(exercise);
    }
}