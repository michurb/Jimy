using Jimy.Application.Abstraction;
using Jimy.Core.Entities;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.WorkoutSessions.Handlers;

public sealed class StartWorkoutSessionHandler: ICommandHandler<StartWorkoutSession>
{
    private readonly IWorkoutSessionRepository _sessionRepository;
    private readonly IWorkoutPlanRepository _planRepository;
    
    public StartWorkoutSessionHandler(
        IWorkoutSessionRepository sessionRepository,
        IWorkoutPlanRepository planRepository)
    {
        _sessionRepository = sessionRepository;
        _planRepository = planRepository;
    }

    public async Task HandleAsync(StartWorkoutSession command)
    {
        var workoutPlanId = new WorkoutPlanId(command.WorkoutPlanId);
        var workoutPlan = await _planRepository.GetByIdAsync(workoutPlanId);
        if (workoutPlan == null)
        {
            throw new WorkoutPlanNotFoundException(command.WorkoutPlanId);
        }

        // Create the workout session with exercises based on the plan
        var workoutSession = new WorkoutSession(
            new WorkoutSessionId(command.Id),
            command.UserId,
            new WorkoutPlanId(command.WorkoutPlanId),
            DateTime.UtcNow,
            command.Exercises.Select(e => 
                new WorkoutSessionExercise(new ExerciseId(e.ExerciseId), new Sets(e.Sets), new Reps(e.Reps), new Weight(e.Weight))
            ).ToList()
        );

        await _sessionRepository.AddAsync(workoutSession);
    }
}