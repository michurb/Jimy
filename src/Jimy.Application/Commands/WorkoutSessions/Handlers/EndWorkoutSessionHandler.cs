using Jimy.Application.Abstraction;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.WorkoutSessions.Handlers;

public sealed class EndWorkoutSessionHandler(IWorkoutSessionRepository workoutSessionRepository) : ICommandHandler<EndWorkoutSession>
{
    public async Task HandleAsync(EndWorkoutSession command)
    {
        var workoutSessionId = new WorkoutSessionId(command.Id);
        var workoutSession = await workoutSessionRepository.GetByIdAsync(workoutSessionId);
        if (workoutSession == null)
        {
            throw new WorkoutSessionNotFoundException(command.Id);
        }

        if (workoutSession.EndTime.HasValue)
        {
            throw new WorkoutSessionAlreadyEndedException(command.Id);
        }

        workoutSession.End(DateTime.UtcNow);

        foreach (var exerciseDto in command.Exercises)
        {
            var exerciseId = new ExerciseId(exerciseDto.ExerciseId);
            workoutSession.UpdateExercise(
                exerciseId,
                new Sets(exerciseDto.Sets),
                new Reps(exerciseDto.Reps),
                new Weight(exerciseDto.Weight)
            );
        }

        await workoutSessionRepository.UpdateAsync(workoutSession);
    }
}