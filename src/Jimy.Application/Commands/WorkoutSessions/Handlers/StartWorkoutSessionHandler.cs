using Jimy.Application.Abstraction;
using Jimy.Core.Entities;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.WorkoutSessions.Handlers;

public sealed class StartWorkoutSessionHandler(IWorkoutSessionRepository workoutSessionRepository,
    IWorkoutPlanRepository workoutPlanRepository,
    IExerciseRepository exerciseRepository) : ICommandHandler<StartWorkoutSession>
{
    public async Task HandleAsync(StartWorkoutSession command)
    {
        var userId = new UserId(command.UserId);
        var workoutPlanId = new WorkoutPlanId(command.WorkoutPlanId);

        var workoutPlan = await workoutPlanRepository.GetByIdAsync(workoutPlanId);
        if (workoutPlan == null)
        {
            throw new WorkoutPlanNotFoundException(command.WorkoutPlanId);
        }

        var workoutSession = new WorkoutSession(
            WorkoutSessionId.NewId(),
            userId,
            workoutPlanId,
            DateTime.UtcNow
        );

        foreach (var exerciseDto in command.Exercises)
        {
            var exerciseId = new ExerciseId(exerciseDto.ExerciseId);
            var exercise = await exerciseRepository.GetByIdAsync(exerciseId);
            if (exercise == null)
            {
                throw new ExerciseNotFoundException(exerciseDto.ExerciseId);
            }

            workoutSession.AddExercise(
                exercise.Id,
                new Sets(exerciseDto.Sets),
                new Reps(exerciseDto.Reps),
                new Weight(exerciseDto.Weight)
            );
        }

        await workoutSessionRepository.AddAsync(workoutSession);
    }
}