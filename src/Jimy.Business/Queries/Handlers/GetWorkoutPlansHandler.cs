using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Queries.Handlers;

public class GetWorkoutPlansHandler : IQueryHandler<GetWorkoutPlans, IEnumerable<WorkoutPlanDto>>
{
    private readonly IMapper _mapper;
    private readonly IWorkoutPlanRepository _repository;

    public GetWorkoutPlansHandler(IWorkoutPlanRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WorkoutPlanDto>> HandleAsync(GetWorkoutPlans query)
    {
        var workoutPlans = await _repository.GetByUserIdAsync(query.UserId);
        return _mapper.Map<IEnumerable<WorkoutPlanDto>>(workoutPlans);
    }
}