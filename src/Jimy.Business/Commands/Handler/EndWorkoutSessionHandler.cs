using Jimy.Business.Abstracition;
using Jimy.Business.Exceptions;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class EndWorkoutSessionHandler : ICommandHandler<EndWorkoutSession>
{
    private readonly IWorkoutSessionRepository _repository;

    public EndWorkoutSessionHandler(IWorkoutSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(EndWorkoutSession command)
    {
        var workoutSession = await _repository.GetByIdAsync(command.WorkoutSessionId);
        if (workoutSession == null)
            throw new WorkoutSessionNotFoundException(command.WorkoutSessionId);

        if (workoutSession.EndTime.HasValue)
            throw new WorkoutSessionAlreadyEndedException(command.WorkoutSessionId);

        workoutSession.EndTime = DateTime.UtcNow;

        // Update the exercises
        foreach (var updatedExercise in command.UpdatedExercises)
        {
            var exercise = workoutSession.Exercises.FirstOrDefault(e => e.Id == updatedExercise.Id);
            if (exercise != null)
            {
                exercise.Weight = updatedExercise.Weight;
            }
        }

        await _repository.UpdateAsync(workoutSession);
    }
}