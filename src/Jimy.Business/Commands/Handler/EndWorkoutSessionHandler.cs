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

        var endTime = DateTime.UtcNow;
        var duration = endTime - workoutSession.StartTime;

        // Cap the duration at 10 hours
        if (duration > TimeSpan.FromHours(10))
        {
            endTime = workoutSession.StartTime.AddHours(10);
        }

        workoutSession.EndTime = endTime;
        await _repository.UpdateAsync(workoutSession);
    }
}