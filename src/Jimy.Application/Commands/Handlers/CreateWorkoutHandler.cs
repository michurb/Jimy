using Jimy.Application.Abstraction;
using Jimy.Core.Entities;
using Jimy.Core.Exceptions;
using Jimy.Core.Interfaces;

namespace Jimy.Application.Commands.Handlers;

public class CreateWorkoutHandler : ICommandHandler<CreateWorkoutPlan, int>
{
    private readonly IWorkoutPlanRepository _workoutRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public CreateWorkoutHandler(IWorkoutPlanRepository workoutRepository, IExerciseRepository exerciseRepository)
    {
        _workoutRepository = workoutRepository;
        _exerciseRepository = exerciseRepository;
    }
    public async Task<int> Handle(CreateWorkoutPlan command)
    {
        var workoutPlan = new WorkoutPlan(command.UserId, command.Name);

        foreach (var exerciseDto in command.Exercises)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(exerciseDto.ExerciseId);
            if (exercise == null)
            {
                throw new ExerciseNotFoundException(exerciseDto.ExerciseId);
            }
            workoutPlan.AddExercise(exercise, exerciseDto.Sets, exerciseDto.Reps);
        }

        await _workoutRepository.AddAsync(workoutPlan);
        await _workoutRepository.UpdateAsync(workoutPlan);

        return workoutPlan.Id;
    }
}