using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.Exceptions;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class CreateWorkoutPlanHandler : ICommandHandler<CreateWorkoutPlan>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;
    private readonly IWorkoutPlanRepository _repository;

    public CreateWorkoutPlanHandler(IWorkoutPlanRepository repository, IExerciseRepository exerciseRepository,
        IMapper mapper)
    {
        _repository = repository;
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task HandleAsync(CreateWorkoutPlan command)
    {
        var workoutPlan = new WorkoutPlan
        {
            UserId = command.Dto.UserId,
            Name = command.Dto.Name,
            CreatedDate = DateTime.UtcNow,
            Exercises = new List<WorkoutExercise>()
        };

        foreach (var exerciseDto in command.Dto.Exercises)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(exerciseDto.ExerciseId);
            if (exercise == null) throw new ExerciseNotFoundException(exerciseDto.ExerciseId);

            var workoutExercise = new WorkoutExercise
            {
                ExerciseId = exercise.Id,
                Sets = exerciseDto.Sets,
                Reps = exerciseDto.Reps
            };

            workoutPlan.Exercises.Add(workoutExercise);
        }

        await _repository.AddAsync(workoutPlan);
    }
}