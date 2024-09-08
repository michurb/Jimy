using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;
using Jimy.Business.Exceptions;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Queries.Handlers;

public class GetWorkoutPlanHandler : IQueryHandler<GetWorkoutPlan, WorkoutPlanDto>
{
    private readonly IMapper _mapper;
    private readonly IWorkoutPlanRepository _repository;

    public GetWorkoutPlanHandler(IWorkoutPlanRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<WorkoutPlanDto> HandleAsync(GetWorkoutPlan query)
    {
        var workoutPlan = await _repository.GetByIdWithExercisesAsync(query.Id);
        if (workoutPlan == null)
            throw new WorkoutPlanNotFoundException(query.Id);

        return _mapper.Map<WorkoutPlanDto>(workoutPlan);
    }
}