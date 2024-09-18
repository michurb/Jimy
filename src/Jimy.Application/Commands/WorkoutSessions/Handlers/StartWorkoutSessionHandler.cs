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
        var workoutPlanId = new WorkoutPlanId(command.WorkoutPlanId);
        var workoutPlan = await _planRepository.GetByIdAsync(workoutPlanId);
        if (workoutPlan == null)
        {
            throw new WorkoutPlanNotFoundException(command.WorkoutPlanId);
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
                    new Weight(1) // Initial weight set to 0
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