using Jimy.Application.Abstraction;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.WorkoutSessions.Handlers;

public sealed class UpdateWorkoutSessionExerciseWeightHandler : ICommandHandler<UpdateWorkoutSessionExerciseWeight>
{
    private readonly IWorkoutSessionRepository _sessionRepository;

    public UpdateWorkoutSessionExerciseWeightHandler(IWorkoutSessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }
    public async Task HandleAsync(UpdateWorkoutSessionExerciseWeight command)
    {
        var sessionId = new WorkoutSessionId(command.SessionId);
        var exerciseId = new ExerciseId(command.ExerciseId);
        var weight = new Weight(command.Weight);
        var setNumber = new Sets(command.SetNumber);
        var session = await _sessionRepository.GetByIdAsync(sessionId);
        if (session == null)
        {
            throw new WorkoutSessionNotFoundException(command.SessionId); // Custom exception
        }
        
        session.UpdateExerciseWeight(exerciseId, setNumber, weight);
        
        await _sessionRepository.UpdateAsync(session);
    }
}