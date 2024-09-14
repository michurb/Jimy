using Jimy.Application.Abstraction;
using Jimy.Core.Entities;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.WorkoutPlans.Handlers;

public sealed class CreateWorkoutPlanHandler(
    IWorkoutPlanRepository workoutPlanRepository,
    IExerciseRepository exerciseRepository)
    : ICommandHandler<CreateWorkoutPlan>
{
    public async Task HandleAsync(CreateWorkoutPlan command)
    {
        var workoutPlan = new WorkoutPlan(
            WorkoutPlanId.NewId(),
            new UserId(command.UserId),
            new WorkoutPlanName(command.Name),
            DateTime.UtcNow
        );

        foreach (var exerciseDto in command.Exercises)
        {
            var exerciseId = new ExerciseId(exerciseDto.ExerciseId);
            var exercise = await exerciseRepository.GetByIdAsync(exerciseId);
            if (exercise == null)
            {
                throw new ExerciseNotFoundException(exerciseId.Value);
            }

            workoutPlan.AddExercise(WorkoutExerciseId.NewId(), exercise.Id, new Sets(exerciseDto.Sets), new Reps(exerciseDto.Reps));
        }

        await workoutPlanRepository.AddAsync(workoutPlan);
    }
}