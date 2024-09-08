using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.Exceptions;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class UpdateWorkoutPlanHandler : ICommandHandler<UpdateWorkoutPlan>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;
    private readonly IWorkoutPlanRepository _repository;

    public UpdateWorkoutPlanHandler(IWorkoutPlanRepository repository, IExerciseRepository exerciseRepository,
        IMapper mapper)
    {
        _repository = repository;
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task HandleAsync(UpdateWorkoutPlan command)
    {
        var workoutPlan = await _repository.GetByIdAsync(command.Id);
        if (workoutPlan == null)
            throw new WorkoutPlanNotFoundException(command.Id);

        workoutPlan.Name = command.Dto.Name;

        // Remove exercises that are not in the update DTO
        workoutPlan.Exercises.RemoveAll(we => !command.Dto.Exercises.Any(e => e.Id == we.Id));

        foreach (var updateExerciseDto in command.Dto.Exercises)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(updateExerciseDto.ExerciseId);
            if (exercise == null) throw new ExerciseNotFoundException(updateExerciseDto.ExerciseId);

            var workoutExercise = workoutPlan.Exercises.FirstOrDefault(we => we.Id == updateExerciseDto.Id);
            if (workoutExercise == null)
            {
                workoutExercise = new WorkoutExercise
                {
                    WorkoutPlanId = workoutPlan.Id,
                    ExerciseId = exercise.Id,
                    Sets = updateExerciseDto.Sets,
                    Reps = updateExerciseDto.Reps
                };
                workoutPlan.Exercises.Add(workoutExercise);
            }
            else
            {
                workoutExercise.Sets = updateExerciseDto.Sets;
                workoutExercise.Reps = updateExerciseDto.Reps;
            }
        }

        await _repository.UpdateAsync(workoutPlan);
    }
}