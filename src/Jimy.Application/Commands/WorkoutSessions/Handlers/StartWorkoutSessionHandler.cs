using Jimy.Application.Abstraction;
using Jimy.Application.Exceptions;
using Jimy.Core.Entities;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.WorkoutSessions.Handlers;

public sealed class StartWorkoutSessionHandler: ICommandHandler<StartWorkoutSession>
{
    private readonly IWorkoutSessionRepository _sessionRepository;
    private readonly IWorkoutPlanRepository _planRepository;
    private readonly IExerciseRepository _exerciseRepository;
    
    public StartWorkoutSessionHandler(
        IWorkoutSessionRepository sessionRepository,
        IWorkoutPlanRepository planRepository,
        IExerciseRepository exerciseRepository)
    {
        _sessionRepository = sessionRepository;
        _planRepository = planRepository;
        _exerciseRepository = exerciseRepository;
    }

    public async Task HandleAsync(StartWorkoutSession command)
    {
        var userId = new UserId(command.UserId);
        var workoutPlanId = new WorkoutPlanId(command.WorkoutPlanId);
        var workoutPlan = await _planRepository.GetByIdAsync(workoutPlanId);
        var activeWrokotuSessions = await _sessionRepository.GetActiveWorkoutSessionsAsync(userId);
        if (workoutPlan == null)
        {
            throw new WorkoutPlanNotFoundException(command.WorkoutPlanId);
        }
        
        if (activeWrokotuSessions.Any())
        {
            throw new ActiveWorkoutSessionAlreadyExistsException();
        }
        
        var exercises = new List<WorkoutSessionExercise>();
        foreach (var planExercise in workoutPlan.Exercises)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(planExercise.ExerciseId);
            if (exercise != null)
            {
                var sessionExercise = new WorkoutSessionExercise(
                    exercise.Id,
                    planExercise.Sets,
                    planExercise.Reps,
                    new Weight(1)
                );

                exercises.Add(sessionExercise);
            }
        }

        var workoutSession = new WorkoutSession(
            new WorkoutSessionId(command.Id),
            new UserId(command.UserId),
            new WorkoutPlanId(command.WorkoutPlanId),
            DateTime.UtcNow,
            exercises
        );

        await _sessionRepository.AddAsync(workoutSession);
    }
}