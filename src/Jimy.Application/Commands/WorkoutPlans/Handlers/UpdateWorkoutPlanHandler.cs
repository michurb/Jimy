using Jimy.Application.Abstraction;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;
using Jimy.Core.ValueObjects;

namespace Jimy.Application.Commands.WorkoutPlans.Handlers;

public sealed class UpdateWorkoutPlanHandler(
    IWorkoutPlanRepository workoutPlanRepository,
    IExerciseRepository exerciseRepository)
    : ICommandHandler<UpdateWorkoutPlan>
{
    public async Task HandleAsync(UpdateWorkoutPlan command)
    {
        var workoutPlanId = new WorkoutPlanId(command.Id);
        var workoutPlan = await workoutPlanRepository.GetByIdAsync(workoutPlanId);
        if (workoutPlan == null)
        {
            throw new WorkoutPlanNotFoundException(workoutPlanId.Value);
        }

        workoutPlan.UpdateName(new WorkoutPlanName(command.Name));
        var updatedExercises = new List<(ExerciseId, Sets, Reps)>();
        foreach (var exerciseDto in command.Exercises)
        {
            var exercise = await exerciseRepository.GetByIdAsync(new ExerciseId(exerciseDto.ExerciseId));
            if (exercise == null)
            {
                throw new ExerciseNotFoundException(exerciseDto.ExerciseId);
            }

            updatedExercises.Add((exercise.Id, new Sets(exerciseDto.Sets), new Reps(exerciseDto.Reps)));
        }
        workoutPlan.UpdateExercises(updatedExercises);
        await workoutPlanRepository.UpdateAsync(workoutPlan);
    }
}